using UnityEngine;

public class GemStoneMatrixUI : MonoBehaviour
{
    [SerializeField] private GameObject _gemStoneSlotsParent = null;
    [SerializeField] private GemSlotUI _gemSlotPrefab = null;

    private GemSlotUI[,] _gemSlotMatrix;
    private GemStoneMatrix _gemStoneMatrix;

    public void RegisterGemStoneMatrix(GemStoneMatrix matrix)
    {
        CreateGemSlotMatrix(matrix.SizeX, matrix.SizeY);

        _gemStoneMatrix = matrix;
        _gemStoneMatrix.OnGemUpdate += UpdateGemSlot;
    }

    private void CreateGemSlotMatrix(int sizeX, int sizeY)
    {
        _gemSlotMatrix = new GemSlotUI[sizeX, sizeY];

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                GemSlotUI gemSlotUI = Instantiate(_gemSlotPrefab, _gemStoneSlotsParent.transform);
                gemSlotUI.gameObject.name = string.Format("GemSlot [{0},{1}]", x, y);

                _gemSlotMatrix[x, y] = gemSlotUI;
            }
        }
    }

    private void UpdateGemSlot(int posX, int posY, GemStone gemStone)
    {
        _gemSlotMatrix[posX, posY].UpdateGemSlot(gemStone);
    }
}
