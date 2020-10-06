[System.Serializable]
public class GemStoneMatrix
{
    public delegate void OnGemStatusUpdate(int posX, int posY, GemStone gemStone);

    public event OnGemStatusUpdate OnGemUpdate = delegate { };

    public int SizeX { get; private set; }
    public int SizeY { get; private set; }

    private GemStone[,] _matrix;

    public GemStoneMatrix(int sizeX, int sizeY)
    {
        _matrix = new GemStone[sizeX, sizeY];

        SizeX = sizeX;
        SizeY = sizeY;
    }

    public void CreateInitialMatrix(SpawnTable gemSpawnTable)
    {
        for (int x = 0; x < SizeX; x++)
        {
            for (int y = 0; y < SizeY; y++)
            {
                GemStone newGem = CreateGemStone(gemSpawnTable, x, y);

                _matrix[x, y] = newGem;
                OnGemUpdate.Invoke(x, y, newGem);
            }
        }
    }

    public void UpdateGemStone(int posX, int posY, GemStone gemStone)
    {
        _matrix[posX, posY] = gemStone;

        OnGemUpdate.Invoke(posX, posY, gemStone);
    }

    public void RelocateEmptySlotsInColumn(int column)
    {
        int currentX = SizeX - 1;

        while (currentX > 0)
        {
            int nullSlots = 0;

            for (int x = currentX; x >= 0; x--)
            {
                if (_matrix[x, column] == null)
                {
                    if (x == 0) { return; }
                    nullSlots++;
                }
                else if (nullSlots > 0)
                {
                    SwapWithEmptySlot(x + nullSlots, x, column);
                    currentX = x;
                    break;
                }
            }
        }
    }

    public void UpdateMatrixColumnDisplay(int column)
    {
        for (int x = 0; x < SizeX; x++)
        {
            OnGemUpdate.Invoke(x, column, _matrix[x, column]);
        }
    }

    public void UpdateMatrixDisplay()
    {
        for (int x = 0; x < SizeX; x++)
        {
            for (int y = 0; y < SizeY; y++)
            {
                OnGemUpdate.Invoke(x, y, _matrix[x, y]);
            }
        }
    }

    private GemStone CreateGemStone(SpawnTable gemSpawnTable, int posX, int posY)
    {
        GemStoneObject random = (GemStoneObject)gemSpawnTable.ChooseRandom();
        GemStone gemStone = new GemStone(random, posX, posY);

        return gemStone;
    }

    private void SwapWithEmptySlot(int emptyX, int posX, int posY)
    {
        _matrix[emptyX, posY] = _matrix[posX, posY];
        _matrix[posX, posY] = null;

        _matrix[emptyX, posY].PositionX = emptyX;
    }
}
