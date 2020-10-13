using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GemStonePooler
{
    [SerializeField] private List<GemStonePool> _gemStonePools = new List<GemStonePool>();

    private Dictionary<GemStone, GemStonePool> _poolerDictionary;

    public void CreatePooler()
    {
        _poolerDictionary = new Dictionary<GemStone, GemStonePool>();

        foreach (GemStonePool pool in _gemStonePools)
        {
            pool.CreatePool();
            _poolerDictionary.Add(pool.GemStone, pool);
        }
    }

    public GemStoneInstance GetFromPooler(GemStone gemStone)
    {
        return _poolerDictionary[gemStone].GetFromPool();
    }

    public void ReturnToPooler(GemStoneInstance instance)
    {
        _poolerDictionary[instance.GetStone].ReturnToPool(instance);
    }
}
