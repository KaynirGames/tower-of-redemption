﻿using UnityEngine;
using System.Collections.Generic;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private SpawnTable _spawnTable = null;
    [SerializeField] private ParticleSystem _spawnEffect = null;
    [SerializeField] private int _spawnAmount = 1;
    [SerializeField] private bool _applyForce = false;
    [SerializeField] private float _forceValue = 1f;

    private SoundController _sounds;

    private void Awake()
    {
        _sounds = GetComponent<SoundController>();
    }

    public void Spawn()
    {
        Spawn((GameObject)_spawnTable.ChooseRandom(),
              _spawnAmount,
              _applyForce);
    }

    public List<GameObject> Spawn(GameObject prefab, int amount, bool applyForce = false)
    {
        if (_spawnEffect != null)
        {
            Instantiate(_spawnEffect.gameObject, transform.position, Quaternion.identity);
        }

        _sounds.PlaySoundOneShot("SpawnObject");

        List<GameObject> spawnedObjects = new List<GameObject>();

        for (int i = 0; i < amount; i++)
        {
            GameObject gameObject = Instantiate(prefab,
                                                transform.position,
                                                Quaternion.identity);

            if (applyForce)
            {
                ApplyForce(gameObject.GetComponent<Rigidbody2D>());
            }

            spawnedObjects.Add(gameObject);
        }

        return spawnedObjects;
    }

    private void ApplyForce(Rigidbody2D rigidbody)
    {
        if (rigidbody != null)
        {
            Vector2 force = new Vector2(Random.Range(-1, 1),
                                        Random.Range(-1, 1)) * _forceValue;

            rigidbody.AddForce(force, ForceMode2D.Impulse);
        }
    }
}
