[System.Serializable]
public class GemStone
{
    public GemStoneObject GemObject { get; private set; }

    public int PositionX { get; set; }
    public int PositionY { get; set; }

    public GemStone(GemStoneObject gemObject, int posX, int posY)
    {
        GemObject = gemObject;

        PositionX = posX;
        PositionY = posY;
    }

    public GemStone(GemStoneObject gemObject) : this(gemObject, 0, 0) { }

    public void SetPosition(int x, int y)
    {
        PositionX = x;
        PositionY = y;
    }

    public bool TryMatch(GemStone gemStone)
    {
        return GemObject.IsMatching(gemStone.GemObject);
    }
}