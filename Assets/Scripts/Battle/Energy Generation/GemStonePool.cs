using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GemStonePool
{
    [SerializeField] private GemStone _gemStone = null;
    [SerializeField] private int _initialPoolSize = 10;

    public GemStone GemStone => _gemStone;

    private Queue<GemStoneInstance> _gemQueue;

    public void CreatePool()
    {
        _gemQueue = new Queue<GemStoneInstance>();

        for (int i = 0; i < _initialPoolSize; i++)
        {
            _gemQueue.Enqueue(new GemStoneInstance(_gemStone));
        }
    }

    public GemStoneInstance GetFromPool()
    {
        if (_gemQueue.Count > 0)
        {
            return _gemQueue.Dequeue();
        }
        else
        {
            return new GemStoneInstance(_gemStone);
        }
    }

    public void ReturnToPool(GemStoneInstance gemStone)
    {
        _gemQueue.Enqueue(gemStone);
    }
}
