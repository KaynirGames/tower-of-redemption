[System.Serializable]
public class GemStoneInstance
{
    public GemStone GetStone { get; private set; }

    public int PositionX { get; set; }
    public int PositionY { get; set; }

    public GemStoneInstance(GemStone gemStone, int posX, int posY)
    {
        GetStone = gemStone;

        PositionX = posX;
        PositionY = posY;
    }

    public GemStoneInstance(GemStone gemStone) : this(gemStone, 0, 0) { }

    public void SetPosition(int x, int y)
    {
        PositionX = x;
        PositionY = y;
    }

    public bool TryMatch(GemStoneInstance instance)
    {
        return GetStone.IsMatching(instance.GetStone);
    }
}