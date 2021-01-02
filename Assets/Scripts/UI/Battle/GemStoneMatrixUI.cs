using UnityEngine;

public class GemstoneMatrixUI : MonoBehaviour
{
    [SerializeField] private GameObject _gemSlotsParent = null;

    private GemstoneMatrix _gemstoneMatrix;
    private GemstoneUI[,] _gemstoneMatrixUI;

    private int _sizeX;
    private int _sizeY;

    private GemstoneUI _gemSlotPrefab;

    private void Start()
    {
        _gemSlotPrefab = AssetManager.Instance.GemSlotPrefab;
    }

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
                GameObject gemSlotObject = Instantiate(_gemSlotPrefab.gameObject, _gemSlotsParent.transform);
                gemSlotObject.name = string.Format("GemSlot [{0},{1}]", x, y);

                _gemstoneMatrixUI[x, y] = gemSlotObject.GetComponent<GemstoneUI>();
            }
        }
    }

    public void UpdateMatrixSlotUI(int x, int y, Gemstone gemstone)
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
