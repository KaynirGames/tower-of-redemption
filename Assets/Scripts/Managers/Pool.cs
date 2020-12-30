using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
    [SerializeField] private GameObject _prefab = null;

    public int Capacity => _poolQueue.Count;

    private Queue<GameObject> _poolQueue;

    public Pool()
    {
        _poolQueue = new Queue<GameObject>();
    }

    public GameObject Prefab => _prefab;

    public void Store(GameObject objectToStore)
    {
        objectToStore.SetActive(false);
        _poolQueue.Enqueue(objectToStore);
    }

    public GameObject Take()
    {
        GameObject gameObject = _poolQueue.Dequeue();
        gameObject.SetActive(true);

        return gameObject;
    }

    public void Clear()
    {
        _poolQueue.Clear();
    }
}
