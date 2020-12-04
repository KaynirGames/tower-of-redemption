using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Таблица вероятностей появления объектов.
/// </summary>
[CreateAssetMenu(fileName = "Undefined SpawnTable", menuName = "Scriptable Objects/Spawn Table")]
public class SpawnTable : ScriptableObject
{
    [SerializeField] private List<SpawnableObject> _spawnableObjects = new List<SpawnableObject>();

    public int TableSize => _spawnableObjects.Count;

    public Object ChooseRandom()
    {
        if (_spawnableObjects.Count == 0)
        {
            Debug.LogWarning($"Таблица вероятностей {name} пуста!");
            return null;
        }

        int totalWeight = CalculateTotalWeight();

        int randomWeight = Random.Range(0, totalWeight);

        int cumulativeWeight = 0;

        for (int i = 0; i < _spawnableObjects.Count; i++)
        {
            cumulativeWeight += _spawnableObjects[i].Weight;

            if (randomWeight <= cumulativeWeight)
            {
                return _spawnableObjects[i].ObjectToSpawn;
            }
        }

        return null;
    }

    public void AddSpawnableObject(Object objectToSpawn, int weight)
    {
        _spawnableObjects.Add(new SpawnableObject(objectToSpawn,
                                                  weight,
                                                  _spawnableObjects.Count));
    }

    public void RemoveSpawnableObject(SpawnableObject spawnableObject)
    {
        _spawnableObjects.Remove(spawnableObject);
    }

    public void RemoveSpawnableObject(int index)
    {
        _spawnableObjects.RemoveAt(index);
    }

    private int CalculateTotalWeight()
    {
        int totalWeight = 0;

        for (int i = 0; i < _spawnableObjects.Count; i++)
        {
            totalWeight += _spawnableObjects[i].Weight;
        }

        return totalWeight;
    }
}