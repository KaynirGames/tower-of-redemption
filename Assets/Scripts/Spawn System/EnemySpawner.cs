using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Room _room = null;
    [SerializeField] private List<SpawnPoint> _enemySpawnPoints = new List<SpawnPoint>(); // Точки появления противника.

    private void Awake()
    {
        Room.OnActiveRoomChange += SpawnEnemies;
    }
    /// <summary>
    /// Инициировать появление врагов при первом заходе в комнату.
    /// </summary>
    private void SpawnEnemies(Room room)
    {
        if (_room != room) return;

        if (_enemySpawnPoints.Count > 0)
        {
            // Закрыть двери комнаты.

            foreach (SpawnPoint point in _enemySpawnPoints)
            {
                point.Spawn();
            }
        }
        Room.OnActiveRoomChange -= SpawnEnemies;
    }
}