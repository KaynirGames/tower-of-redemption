using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GemStonePooler
{
    [SerializeField] private List<GemStonePool> _gemStonePools = new List<GemStonePool>();

    private Dictionary<GemStoneObject, GemStonePool> _poolerDictionary;

    public void CreatePooler()
    {
        _poolerDictionary = new Dictionary<GemStoneObject, GemStonePool>();

        foreach (GemStonePool pool in _gemStonePools)
        {
            pool.CreatePool();
            _poolerDictionary.Add(pool.GemStoneObject, pool);
        }
    }

    public GemStone GetFromPooler(GemStoneObject gemObject)
    {
        return _poolerDictionary[gemObject].GetFromPool();
    }

    public void ReturnToPooler(GemStone gemStone)
    {
        _poolerDictionary[gemStone.GemObject].ReturnToPool(gemStone);
    }
}
