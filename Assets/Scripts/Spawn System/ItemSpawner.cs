using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> _itemSpawnPoints = new List<SpawnPoint>(); // Точки появления предмета.

    private void Awake()
    {
        DungeonStageManager.OnStageLoaded += SpawnItems;
    }
    /// <summary>
    /// Инициировать появление предметов после загрузки этажа.
    /// </summary>
    private void SpawnItems()
    {
        Debug.Log("Предметы созданы.");
    }
}
