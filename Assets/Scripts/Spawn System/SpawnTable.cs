using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Таблица вероятностей появления объектов.
/// </summary>
[CreateAssetMenu(fileName = "NewSpawnTable", menuName = "Scriptable Objects/Spawn Table")]
public class SpawnTable : ScriptableObject
{
    [SerializeField] private List<SpawnableObject> _spawnableObjects = new List<SpawnableObject>();
    /// <summary>
    /// Создаваемые объекты в таблице вероятностей появления.
    /// </summary>
    public SpawnableObject[] SpawnableObjects => _spawnableObjects.ToArray();
    /// <summary>
    /// Выбрать случайный объект на основе вероятности его появления.
    /// </summary>
    public Object ChooseRandom()
    {
        if (_spawnableObjects.Count == 0)
        {
            Debug.LogWarning($"Таблица вероятностей {name} пуста!");
            return null;
        }

        // Пример: таблица вероятностей появления { (obj1, 50), (obj2, 30), (obj3, 40), (obj4, 10) }
        // Суммарный вес: 50 + 30 + 40 + 10 = 130
        // Случайный вес: 65
        // Первая итерация => Cовокупный вес: 0 + 50 = 50, 65 <= 50 ? Нет.
        // Вторая итерация => Cовокупный вес: 50 + 30 = 80, 65 <= 80 ? Да, значит выбираем второй объект.

        int totalWeight = GetTotalWeight();

        int randomWeight = Random.Range(0, totalWeight);

        int cumulativeWeight = 0;

        for (int i = 0; i < _spawnableObjects.Count; i++)
        {
            cumulativeWeight += _spawnableObjects[i].Weight;

            if (randomWeight <= cumulativeWeight)
            {
                return _spawnableObjects[i].Object;
            }
        }

        return null;
    }
    /// <summary>
    /// Добавить создаваемый объект в таблицу вероятностей появления.
    /// </summary>
    public void Add(Object objectToSpawn, int weight)
    {
        _spawnableObjects.Add(new SpawnableObject(objectToSpawn, weight));
    }
    /// <summary>
    /// Убрать создаваемый объект из таблицы верояностей появления.
    /// </summary>
    public void Remove(SpawnableObject spawnableObject)
    {
        _spawnableObjects.Remove(spawnableObject);
    }
    /// <summary>
    /// Посчитать общую вероятность появления объектов.
    /// </summary>
    /// <returns></returns>
    private int GetTotalWeight()
    {
        int totalWeight = 0;

        for (int i = 0; i < _spawnableObjects.Count; i++)
        {
            totalWeight += _spawnableObjects[i].Weight;
        }

        return totalWeight;
    }
}
