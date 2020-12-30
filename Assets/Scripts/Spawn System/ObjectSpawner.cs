using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnDelay = 0f;
    [SerializeField] private List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();

    private WaitForSeconds _waitForSpawn;

    private void Start()
    {
        _waitForSpawn = new WaitForSeconds(_spawnDelay);
    }

    public void SpawnObjects()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        yield return _waitForSpawn;

        _spawnPoints.ForEach(point => point.Spawn());
    }

    private void OnValidate()
    {
        _spawnPoints = new List<SpawnPoint>(GetComponentsInChildren<SpawnPoint>());
    }
}
