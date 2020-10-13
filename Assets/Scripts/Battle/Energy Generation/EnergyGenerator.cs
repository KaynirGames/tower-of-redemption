using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyGenerator : MonoBehaviour
{
    [SerializeField] private GemStoneMatrix _gemMatrix = new GemStoneMatrix();
    [SerializeField] private GemStoneMatrixUI _gemMatrixUI = null;
    [Header("Параметры матрицы камней:")]
    [SerializeField] private Vector2Int _gemMatrixSize = new Vector2Int(4, 8);
    [SerializeField] private int _minGemConsumeAmount = 2;
    [SerializeField] private float _matrixUpdateUIDelay = 0.2f;

    public bool IsSelectingGems { get; set; }

    private List<GemStoneInstance> _matchingGems = new List<GemStoneInstance>();
    private List<int> _changedColumns = new List<int>();

    private WaitForSeconds _matrixUpdateWaitForSeconds;

    private void Awake()
    {
        SetupGenerator();
        _matrixUpdateWaitForSeconds = new WaitForSeconds(_matrixUpdateUIDelay);
    }

    public void SetupGenerator()
    {
        _gemMatrix.CreateInitialMatrix(_gemMatrixSize.x, _gemMatrixSize.y);
        _gemMatrixUI.RegisterGemStoneMatrix(_gemMatrix);
        _gemMatrix.UpdateMatrixDisplay();
    }

    public bool TrySelectGem(GemStoneInstance instance)
    {
        if (_matchingGems.Count > 0)
        {
            if (_matchingGems.Contains(instance) || !_matchingGems[0].TryMatch(instance))
            {
                ClearGemSelection();
                return false;
            }
        }

        _matchingGems.Add(instance);

        return true;
    }

    public void ConsumeMatchingGems()
    {
        if (_matchingGems.Count >= _minGemConsumeAmount)
        {
            float energyGain = 0f;
            _changedColumns.Clear();

            foreach (GemStoneInstance gem in _matchingGems)
            {
                energyGain += gem.GetStone.EnergyGainValue;

                HandleGemStoneDisposal(gem);
            }

            UpdateEmptySlotsInColumns();

            PlayerManager.Instance.Player.Stats.ChangeEnergy(energyGain);
        }

        ClearGemSelection();
    }

    public void ClearGemSelection()
    {
        _matchingGems.Clear();
        IsSelectingGems = false;
    }

    private void HandleGemStoneDisposal(GemStoneInstance instance)
    {
        _gemMatrix.UpdateMatrixSlot(instance.PositionX, instance.PositionY, null, true);

        if (!_changedColumns.Contains(instance.PositionY))
        {
            _changedColumns.Add(instance.PositionY);
        }

        _gemMatrix.GemPooler.ReturnToPooler(instance);
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
                GemStoneInstance instance = _gemMatrix.CreateGemStoneInstance(x, column);

                _gemMatrix.UpdateMatrixSlot(x, column, instance);
            }
        }

        yield return _matrixUpdateWaitForSeconds;

        _gemMatrix.UpdateMatrixColumnDisplay(column);
    }
}
