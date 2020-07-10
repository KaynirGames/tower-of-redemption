using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private List<Enemy> _currentEnemies = new List<Enemy>();

    public static EnemyManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Enemy.OnEnemyDeath += UnregisterEnemy;
    }

    public void RegisterEnemy(Enemy enemy)
    {
        _currentEnemies.Add(enemy);
    }

    private void UnregisterEnemy(Enemy enemy)
    {
        _currentEnemies.Remove(enemy);

        if (_currentEnemies.Count == 0)
        {
            // Сообщаем, что бой в комнате завершился.
        }
    }
}
