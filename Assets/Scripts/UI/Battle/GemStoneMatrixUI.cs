using UnityEngine;

public class GemstoneMatrixUI : MonoBehaviour
{
    [SerializeField] private GameObject _gemStoneSlotsParent = null;
    [SerializeField] private GemstoneUI _gemSlotPrefab = null;

    private GemstoneMatrix _gemstoneMatrix;
    private GemstoneUI[,] _gemstoneMatrixUI;

    private int _sizeX;
    private int _sizeY;

    public void DeselectGemSlotUI(int posX, int posY)
    {
        _gemstoneMatrixUI[posX, posY].ToggleSelectionAnimation(false);
    }

    public void CreateMatrixUI(int sizeX, int sizeY, GemstoneMatrix gemstoneMatrix)
    {
        _gemstoneMatrix = gemstoneMatrix;
        _gemstoneMatrixUI = new GemstoneUI[sizeX, sizeY];

        _sizeX = sizeX;
        _sizeY = sizeY;

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                GemstoneUI gemSlotUI = Instantiate(_gemSlotPrefab, _gemStoneSlotsParent.transform);
                gemSlotUI.gameObject.name = string.Format("GemSlot [{0},{1}]", x, y);

                _gemstoneMatrixUI[x, y] = gemSlotUI;
            }
        }
    }

    public void UpdateMatrixSlotUI(int x, int y, GemstoneInstance gemstone)
    {
        _gemstoneMatrixUI[x, y].UpdateDisplay(gemstone);
    }

    public void UpdateMatrixColumnUI(int column)
    {
        for (int x = 0; x < _sizeX; x++)
        {
            UpdateMatrixSlotUI(x, column, _gemstoneMatrix.GetGemstone(x, column));
        }
    }

    public void UpdateMatrixUI()
    {
        for (int x = 0; x < _sizeX; x++)
        {
            for (int y = 0; y < _sizeY; y++)
            {
                UpdateMatrixSlotUI(x, y, _gemstoneMatrix.GetGemstone(x, y));
            }
        }
    }

    public void ResetSelection(int x, int y)
    {
        _gemstoneMatrixUI[x, y].ToggleSelectionAnimation(false);
    }
}
