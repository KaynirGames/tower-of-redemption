[System.Serializable]
public class GemstoneInstance
{
    public Gemstone Gemstone { get; private set; }

    public int X { get; set; }
    public int Y { get; set; }

    public GemstoneInstance(Gemstone gemstone, int x, int y)
    {
        Gemstone = gemstone;

        X = x;
        Y = y;
    }

    public GemstoneInstance(Gemstone gemStone) : this(gemStone, 0, 0) { }

    public void SetPosition(int x, int y)
    {
        X = x;
        Y = y;
    }

    public bool CheckMatch(Gemstone gemstone)
    {
        return Gemstone.IsMatching(gemstone);
    }
}