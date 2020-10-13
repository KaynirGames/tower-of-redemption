using UnityEngine;

[System.Serializable]
public class GemStoneMatrix
{
    public delegate void OnMatrixSlotUpdate(int posX, int posY, GemStoneInstance gemStone);

    public event OnMatrixSlotUpdate OnSlotUpdate = delegate { };

    [SerializeField] private SpawnTable _gemSpawnTable = null;
    [SerializeField] private GemStonePooler _gemPooler = null;

    public int SizeX { get; private set; }
    public int SizeY { get; private set; }

    public GemStonePooler GemPooler => _gemPooler;

    private GemStoneInstance[,] _matrix;

    public void CreateInitialMatrix(int sizeX, int sizeY)
    {
        _matrix = new GemStoneInstance[sizeX, sizeY];
        _gemPooler.CreatePooler();

        SizeX = sizeX;
        SizeY = sizeY;

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                GemStoneInstance newGem = CreateGemStoneInstance(x, y);

                _matrix[x, y] = newGem;
            }
        }
    }

    public GemStoneInstance CreateGemStoneInstance(int posX, int posY)
    {
        GemStone random = (GemStone)_gemSpawnTable.ChooseRandom();
        GemStoneInstance gemStone = _gemPooler.GetFromPooler(random);

        gemStone.SetPosition(posX, posY);

        return gemStone;
    }

    public void UpdateMatrixSlot(int posX, int posY, GemStoneInstance instance)
    {
        _matrix[posX, posY] = instance;
    }

    public void UpdateMatrixSlot(int posX, int posY, GemStoneInstance instance, bool updateDisplay)
    {
        UpdateMatrixSlot(posX, posY, instance);

        if (updateDisplay) { UpdateMatrixSlotDisplay(posX, posY); }
    }

    public bool CheckForEmptySlot(int posX, int posY)
    {
        return _matrix[posX, posY] == null;
    }

    public void RelocateEmptySlotsInColumn(int column)
    {
        int currentX = SizeX - 1;

        while (currentX > 0)
        {
            int nullSlots = 0;

            for (int x = currentX; x >= 0; x--)
            {
                if (CheckForEmptySlot(x, column))
                {
                    if (x == 0) { return; }
                    nullSlots++;
                }
                else if (nullSlots > 0)
                {
                    SwapWithEmptySlot(x + nullSlots, x, column);
                    currentX = x + nullSlots - 1;
                    break;
                }
            }
        }
    }

    public void UpdateMatrixSlotDisplay(int x, int y)
    {
        OnSlotUpdate.Invoke(x, y, _matrix[x, y]);
    }

    public void UpdateMatrixColumnDisplay(int column)
    {
        for (int x = 0; x < SizeX; x++)
        {
            OnSlotUpdate.Invoke(x, column, _matrix[x, column]);
        }
    }

    public void UpdateMatrixDisplay()
    {
        for (int x = 0; x < SizeX; x++)
        {
            for (int y = 0; y < SizeY; y++)
            {
                OnSlotUpdate.Invoke(x, y, _matrix[x, y]);
            }
        }
    }

    private void SwapWithEmptySlot(int emptyX, int posX, int posY)
    {
        _matrix[emptyX, posY] = _matrix[posX, posY];
        _matrix[posX, posY] = null;

        _matrix[emptyX, posY].PositionX = emptyX;
    }
}
