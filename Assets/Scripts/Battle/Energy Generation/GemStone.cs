[System.Serializable]
public class Gemstone
{
    public GemstoneSO GemstoneSO { get; private set; }

    public int X { get; set; }
    public int Y { get; set; }

    public Gemstone(GemstoneSO gemstoneSO, int x, int y)
    {
        GemstoneSO = gemstoneSO;

        X = x;
        Y = y;
    }

    public Gemstone(GemstoneSO gemStoneSO) : this(gemStoneSO, 0, 0) { }

    public void SetPosition(int x, int y)
    {
        X = x;
        Y = y;
    }

    public bool CheckMatch(GemstoneSO gemstoneSO)
    {
        return GemstoneSO.IsMatching(gemstoneSO);
    }
}