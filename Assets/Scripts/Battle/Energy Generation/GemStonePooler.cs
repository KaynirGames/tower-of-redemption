using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GemstonePooler
{
    [SerializeField] private List<GemstonePool> _gemStonePools = new List<GemstonePool>();

    private Dictionary<GemstoneSO, GemstonePool> _poolerDictionary;

    public void CreatePooler()
    {
        _poolerDictionary = new Dictionary<GemstoneSO, GemstonePool>();

        foreach (GemstonePool pool in _gemStonePools)
        {
            pool.CreatePool();
            _poolerDictionary.Add(pool.GemstoneSO, pool);
        }
    }

    public Gemstone GetFromPooler(GemstoneSO gemstoneSO)
    {
        return _poolerDictionary[gemstoneSO].GetFromPool();
    }

    public void ReturnToPooler(Gemstone gemstone)
    {
        _poolerDictionary[gemstone.GemstoneSO].ReturnToPool(gemstone);
    }
}
