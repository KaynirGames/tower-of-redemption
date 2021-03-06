﻿using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    [SerializeField] private List<Pool> _pools = new List<Pool>();

    private Dictionary<string, Pool> _poolDictionary;

    private void Awake()
    {
        Instance = this;
        CreatePools();
    }

    public void Store(GameObject objectToStore)
    {
        if (!Exist(objectToStore.tag)) { return; }

        GetPool(objectToStore.tag).Store(objectToStore);
    }

    public GameObject Take(string tag)
    {
        if (!Exist(tag)) { return null; }

        Pool pool = GetPool(tag);

        if (pool.Capacity == 0)
        {
            ExpandPool(pool, 1);
        }

        return pool.Take();
    }

    public void ClearPools()
    {
        foreach (var pool in _poolDictionary)
        {
            pool.Value.Clear();
        }
    }

    private void ExpandPool(Pool pool, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject newObject = Instantiate(pool.Prefab);
            newObject.SetActive(false);
            pool.Store(newObject);
        }
    }

    private bool Exist(string tag)
    {
        return _poolDictionary.ContainsKey(tag);
    }

    private Pool GetPool(string tag)
    {
        return _poolDictionary[tag];
    }

    private void CreatePools()
    {
        _poolDictionary = new Dictionary<string, Pool>();

        foreach (Pool pool in _pools)
        {
            if (!_poolDictionary.ContainsKey(pool.Prefab.tag))
            {
                _poolDictionary.Add(pool.Prefab.tag, pool);
            }
        }
    }
}
