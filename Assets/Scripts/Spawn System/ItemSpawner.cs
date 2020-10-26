using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> _itemSpawnPoints = new List<SpawnPoint>(); // Точки появления предмета.

    private void Awake()
    {
        DungeonStageManager.OnStageLoadComplete += SpawnItems;
    }

    private void SpawnItems(DungeonStage dungeonStage)
    {
        Debug.Log("Предметы созданы.");
    }
}
