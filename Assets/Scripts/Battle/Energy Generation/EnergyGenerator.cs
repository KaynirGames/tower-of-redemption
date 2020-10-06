using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyGenerator : MonoBehaviour
{
    [SerializeField] private Vector2Int _gemMatrixSize = new Vector2Int(4, 8);
    [SerializeField] private SpawnTable _gemSpawnTable = null;
    [SerializeField] private GemStoneMatrixUI _gemMatrixUI = null;
    [SerializeField] private int _minGemConsumeAmount = 2;

    public bool IsSelectingGems { get; set; }

    private GemStoneMatrix _gemMatrix;

    private List<GemStone> _gemPool = new List<GemStone>();
    private List<GemStone> _matchingGems = new List<GemStone>();
    private List<int> _changedColumns = new List<int>();

    private void Awake()
    {
        SetupGenerator();
    }

    public void SetupGenerator()
    {
        _gemMatrix = new GemStoneMatrix(_gemMatrixSize.x, _gemMatrixSize.y);
        _gemMatrixUI.RegisterGemStoneMatrix(_gemMatrix);

        _gemMatrix.CreateInitialMatrix(_gemSpawnTable);
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
                _gemMatrix.UpdateGemStone(gem.PositionX, gem.PositionY, null);

                if (!_changedColumns.Contains(gem.PositionY))
                {
                    _changedColumns.Add(gem.PositionY);
                }
            }

            UpdateEmptySlots();

            PlayerManager.Instance.Player.Stats.ChangeEnergy(energyGain);
        }

        ClearGemSelection();
    }

    public void ClearGemSelection()
    {
        _matchingGems.Clear();
        IsSelectingGems = false;
    }

    private void UpdateEmptySlots()
    {
        foreach (int col in _changedColumns)
        {
            _gemMatrix.RelocateEmptySlotsInColumn(col);

            _gemMatrix.UpdateMatrixColumnDisplay(col);
        }
    }

    private GemStone GetPoolGem()
    {
        return _gemPool[Random.Range(0, _gemPool.Count)];
    }
}
