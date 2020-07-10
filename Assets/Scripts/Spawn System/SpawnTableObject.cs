using UnityEngine;

/// <summary>
/// Элемент в таблице вероятностей появления объектов.
/// </summary>
[System.Serializable]
public class SpawnTableObject
{
    /// <summary>
    /// Объект для появления.
    /// </summary>
    public Object ObjectToSpawn;
    /// <summary>
    /// Вес объекта в таблице вероятностей.
    /// </summary>
    public int Weight;
}
