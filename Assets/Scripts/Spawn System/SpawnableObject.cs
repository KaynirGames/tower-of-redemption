using UnityEngine;

/// <summary>
/// Создаваемый объект в таблице вероятностей появления.
/// </summary>
[System.Serializable]
public class SpawnableObject
{
    [SerializeField] private Object _object = null;
    [SerializeField] private int _weight = 0;
    /// <summary>
    /// Создаваемый объект.
    /// </summary>
    public Object Object => _object;
    /// <summary>
    /// Вес объекта в таблице вероятностей.
    /// </summary>
    public int Weight => _weight;

    public SpawnableObject(Object objectToSpawn, int weight)
    {
        _object = objectToSpawn;
        _weight = weight;
    }
}
