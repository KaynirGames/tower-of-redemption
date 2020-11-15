using KaynirGames.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyGenerator : MonoBehaviour
{
    [SerializeField] private GemstoneMatrix _gemMatrix = null;
    [SerializeField] private GemstoneMatrixUI _gemMatrixUI = null;
    [SerializeField] private FloatingTextPopup _energyGainTextPopup = null;
    [Header("Параметры матрицы камней:")]
    [SerializeField] private Vector2Int _gemMatrixSize = new Vector2Int(4, 8);
    [SerializeField] private int _minGemstonesForConsume = 2;
    [SerializeField] private float _timeForMatrixUpdate = 0.25f;

    public bool IsSelectingGems { get; set; }

    private List<Gemstone> _matchingGemstones = new List<Gemstone>();
    private List<int> _changedColumns = new List<int>();

    private WaitForSeconds _waitForMatrixUpdate;

    private void Start()
    {
        _waitForMatrixUpdate = new WaitForSeconds(_timeForMatrixUpdate);
        SetupGenerator();
    }

    public void SetupGenerator()
    {
        _gemMatrix.CreateMatrix(_gemMatrixSize.x, _gemMatrixSize.y);
        _gemMatrixUI.CreateMatrixUI(_gemMatrixSize.x, _gemMatrixSize.y, _gemMatrix);
        _gemMatrixUI.UpdateMatrixUI();
    }

    public bool TrySelectGemstone(Gemstone gemstone)
    {
        if (_matchingGemstones.Count > 0)
        {
            if (_matchingGemstones.Contains(gemstone) || !_matchingGemstones[0].CheckMatch(gemstone.GemstoneSO))
            {
                ClearGemSelection();
                return false;
            }
        }

        _matchingGemstones.Add(gemstone);

        return true;
    }

    public void ConsumeMatchingGemstones()
    {
        if (_matchingGemstones.Count >= _minGemstonesForConsume)
        {
            _changedColumns.Clear();
            float energyGain = 0f;

            foreach (Gemstone gem in _matchingGemstones)
            {
                energyGain += gem.GemstoneSO.EnergyGainValue;
                HandleGemstoneDisposal(gem);
            }

            UpdateEmptySlotsInColumns();

            PlayerManager.Instance.Player.Stats.ChangeEnergy(energyGain);

            ShowEnergyTextPopup(energyGain);
        }

        ClearGemSelection();
    }

    public void ClearGemSelection()
    {
        foreach (Gemstone gem in _matchingGemstones)
        {
            _gemMatrixUI.ResetSelection(gem.X, gem.Y);
        }

        _matchingGemstones.Clear();
    }

    private void HandleGemstoneDisposal(Gemstone gemstone)
    {
        _gemMatrix.UpdateMatrixSlot(gemstone.X, gemstone.Y, null);
        _gemMatrixUI.UpdateMatrixSlotUI(gemstone.X, gemstone.Y, null);

        if (!_changedColumns.Contains(gemstone.Y))
        {
            _changedColumns.Add(gemstone.Y);
        }

        _gemMatrix.GemstonePooler.ReturnToPooler(gemstone);
    }

    private void UpdateEmptySlotsInColumns()
    {
        foreach (int col in _changedColumns)
        {
            _gemMatrix.RelocateEmptySlotsInColumn(col);
            StartCoroutine(PopulateEmptySlotsInColumn(col));
        }
    }

    private IEnumerator PopulateEmptySlotsInColumn(int column)
    {
        for (int x = 0; x < _gemMatrixSize.x; x++)
        {
            if (_gemMatrix.CheckForEmptySlot(x, column))
            {
                Gemstone gemstone = _gemMatrix.CreateGemstoneInstance(x, column);
                _gemMatrix.UpdateMatrixSlot(x, column, gemstone);
            }
        }

        yield return _waitForMatrixUpdate;

        _gemMatrixUI.UpdateMatrixColumnUI(column);
    }

    private void ShowEnergyTextPopup(float energyGain)
    {
        float fontSize = energyGain > 10f
            ? _energyGainTextPopup.FontSize + Mathf.Round(energyGain / 10f)
            : _energyGainTextPopup.FontSize;

        _energyGainTextPopup.Create(energyGain.ToString("+0"),
                                    KaynirTools.GetPointerWorldPosition(),
                                    fontSize);
    }
}
