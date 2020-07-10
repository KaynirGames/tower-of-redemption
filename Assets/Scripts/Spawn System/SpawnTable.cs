using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Таблица вероятностей появления объектов.
/// </summary>
[CreateAssetMenu(fileName = "NewSpawnTable", menuName = "Scriptable Objects/Spawn Table")]
public class SpawnTable : ScriptableObject
{
    /// <summary>
    /// Список элементов таблицы вероятностей появления объектов.
    /// </summary>
    public List<SpawnTableObject> SpawnTableObjects = new List<SpawnTableObject>();
    /// <summary>
    /// Выбрать случайный объект на основе вероятности его появления.
    /// </summary>
    /// <returns></returns>
    public Object ChooseRandom()
    {
        if (SpawnTableObjects.Count == 0)
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

        for (int i = 0; i < SpawnTableObjects.Count; i++)
        {
            cumulativeWeight += SpawnTableObjects[i].Weight;

            if (randomWeight <= cumulativeWeight)
            {
                return SpawnTableObjects[i].ObjectToSpawn;
            }
        }

        return null;
    }
    /// <summary>
    /// Посчитать общую вероятность появления объектов.
    /// </summary>
    /// <returns></returns>
    private int GetTotalWeight()
    {
        int totalWeight = 0;

        for (int i = 0; i < SpawnTableObjects.Count; i++)
        {
            totalWeight += SpawnTableObjects[i].Weight;
        }

        return totalWeight;
    }
}
