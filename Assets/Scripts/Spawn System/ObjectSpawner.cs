using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();

    public void SpawnObjects()
    {
        _spawnPoints.ForEach(point => point.Spawn());
    }

    private void OnValidate()
    {
        _spawnPoints = new List<SpawnPoint>(GetComponentsInChildren<SpawnPoint>());
    }
}
