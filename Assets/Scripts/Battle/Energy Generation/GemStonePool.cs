using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GemstonePool
{
    [SerializeField] private GemstoneSO _gemStoneSO = null;
    [SerializeField] private int _initialPoolSize = 10;

    public GemstoneSO GemstoneSO => _gemStoneSO;

    private Queue<Gemstone> _gemstoneQueue;

    public void CreatePool()
    {
        _gemstoneQueue = new Queue<Gemstone>();

        for (int i = 0; i < _initialPoolSize; i++)
        {
            _gemstoneQueue.Enqueue(new Gemstone(_gemStoneSO));
        }
    }

    public Gemstone GetFromPool()
    {
        if (_gemstoneQueue.Count > 0)
        {
            return _gemstoneQueue.Dequeue();
        }
        else
        {
            return new Gemstone(_gemStoneSO);
        }
    }

    public void ReturnToPool(Gemstone gemStone)
    {
        _gemstoneQueue.Enqueue(gemStone);
    }
}
