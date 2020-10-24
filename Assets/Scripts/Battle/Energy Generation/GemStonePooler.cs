using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GemstonePooler
{
    [SerializeField] private List<GemstonePool> _gemStonePools = new List<GemstonePool>();

    private Dictionary<Gemstone, GemstonePool> _poolerDictionary;

    public void CreatePooler()
    {
        _poolerDictionary = new Dictionary<Gemstone, GemstonePool>();

        foreach (GemstonePool pool in _gemStonePools)
        {
            pool.CreatePool();
            _poolerDictionary.Add(pool.GemStone, pool);
        }
    }

    public GemstoneInstance GetFromPooler(Gemstone gemStone)
    {
        return _poolerDictionary[gemStone].GetFromPool();
    }

    public void ReturnToPooler(GemstoneInstance instance)
    {
        _poolerDictionary[instance.Gemstone].ReturnToPool(instance);
    }
}
