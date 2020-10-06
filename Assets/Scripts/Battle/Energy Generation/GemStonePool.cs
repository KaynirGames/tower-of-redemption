using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GemStonePool
{
    [SerializeField] private GemStoneObject _gemStoneObject = null;
    [SerializeField] private int _initialPoolSize = 10;

    public GemStoneObject GemStoneObject => _gemStoneObject;

    private Queue<GemStone> _gemQueue;

    public void CreatePool()
    {
        _gemQueue = new Queue<GemStone>();

        for (int i = 0; i < _initialPoolSize; i++)
        {
            _gemQueue.Enqueue(new GemStone(_gemStoneObject));
        }
    }

    public GemStone GetFromPool()
    {
        if (_gemQueue.Count > 0)
        {
            return _gemQueue.Dequeue();
        }
        else
        {
            return new GemStone(_gemStoneObject);
        }
    }

    public void ReturnToPool(GemStone gemStone)
    {
        _gemQueue.Enqueue(gemStone);
    }
}
