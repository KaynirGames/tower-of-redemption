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

    private List<GemStone> _matchingGems = new List<GemStone>();
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

    public bool TrySelectGem(GemStone gemStone)
    {
        if (_matchingGems.Count > 0)
        {
            if (_matchingGems.Contains(gemStone) || !_matchingGems[0].TryMatch(gemStone))
            {
                ClearGemSelection();
                return false;
            }
        }

        _matchingGems.Add(gemStone);

        return true;
    }

    public void ConsumeMatchingGems()
    {
        if (_matchingGems.Count >= _minGemConsumeAmount)
        {
            float energyGain = 0f;
            _changedColumns.Clear();

            foreach (GemStone gem in _matchingGems)
            {
                energyGain += gem.GemObject.EnergyGainValue;

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

    private void HandleGemStoneDisposal(GemStone gem)
    {
        _gemMatrix.UpdateMatrixSlot(gem.PositionX, gem.PositionY, null, true);

        if (!_changedColumns.Contains(gem.PositionY))
        {
            _changedColumns.Add(gem.PositionY);
        }

        _gemMatrix.GemPooler.ReturnToPooler(gem);
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
                GemStone gemStone = _gemMatrix.CreateGemStone(x, column);

                _gemMatrix.UpdateMatrixSlot(x, column, gemStone);
            }
        }

        yield return _matrixUpdateWaitForSeconds;

        _gemMatrix.UpdateMatrixColumnDisplay(column);
    }
}
