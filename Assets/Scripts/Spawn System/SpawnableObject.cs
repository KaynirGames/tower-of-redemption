using UnityEngine;

[System.Serializable]
public class SpawnableObject
{
    [SerializeField] private Object _objectToSpawn = null;
    [SerializeField] private int _weight = 0;
    [SerializeField] private int _index = 0;

    public Object ObjectToSpawn => _objectToSpawn;
    public int Weight => _weight;
    public int Index => _index;

    public SpawnableObject(Object objectToSpawn, int weight, int index)
    {
        _objectToSpawn = objectToSpawn;
        _weight = weight;
        _index = index;
    }
}
