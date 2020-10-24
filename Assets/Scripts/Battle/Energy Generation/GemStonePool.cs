using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GemstonePool
{
    [SerializeField] private Gemstone _gemStone = null;
    [SerializeField] private int _initialPoolSize = 10;

    public Gemstone GemStone => _gemStone;

    private Queue<GemstoneInstance> _gemQueue;

    public void CreatePool()
    {
        _gemQueue = new Queue<GemstoneInstance>();

        for (int i = 0; i < _initialPoolSize; i++)
        {
            _gemQueue.Enqueue(new GemstoneInstance(_gemStone));
        }
    }

    public GemstoneInstance GetFromPool()
    {
        if (_gemQueue.Count > 0)
        {
            return _gemQueue.Dequeue();
        }
        else
        {
            return new GemstoneInstance(_gemStone);
        }
    }

    public void ReturnToPool(GemstoneInstance gemStone)
    {
        _gemQueue.Enqueue(gemStone);
    }
}
