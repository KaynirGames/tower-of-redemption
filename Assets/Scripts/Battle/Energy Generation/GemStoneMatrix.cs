using UnityEngine;

[System.Serializable]
public class GemstoneMatrix
{
    [SerializeField] private SpawnTable _gemstoneSpawnTable = null;
    [SerializeField] private GemstonePooler _gemstonePooler = null;

    public GemstonePooler GemstonePooler => _gemstonePooler;

    private Gemstone[,] _matrix;

    public void CreateMatrix(int sizeX, int sizeY)
    {
        _matrix = new Gemstone[sizeX, sizeY];

        _gemstonePooler.CreatePooler();

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                Gemstone newGem = CreateGemstoneInstance(x, y);

                _matrix[x, y] = newGem;
            }
        }
    }

    public Gemstone CreateGemstoneInstance(int x, int y)
    {
        GemstoneSO random = (GemstoneSO)_gemstoneSpawnTable.ChooseRandom();
        Gemstone gemStone = _gemstonePooler.GetFromPooler(random);

        gemStone.SetPosition(x, y);

        return gemStone;
    }

    public void UpdateMatrixSlot(int x, int y, Gemstone gemstone)
    {
        _matrix[x, y] = gemstone;
    }

    public bool CheckForEmptySlot(int x, int y)
    {
        return _matrix[x, y] == null;
    }

    public void RelocateEmptySlotsInColumn(int column)
    {
        int currentX = _matrix.GetLength(0) - 1;

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

    public Gemstone GetGemstone(int x, int y)
    {
        return _matrix[x, y];
    }

    private void SwapWithEmptySlot(int emptyX, int posX, int posY)
    {
        _matrix[emptyX, posY] = _matrix[posX, posY];
        _matrix[posX, posY] = null;

        _matrix[emptyX, posY].X = emptyX;
    }
}
