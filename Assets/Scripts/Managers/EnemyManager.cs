using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private List<EnemyCharacter> _currentEnemies = new List<EnemyCharacter>();

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
    }

    public void RegisterEnemy(EnemyCharacter enemy)
    {
        _currentEnemies.Add(enemy);
    }

    private void UnregisterEnemy(EnemyCharacter enemy)
    {
        _currentEnemies.Remove(enemy);

        if (_currentEnemies.Count == 0)
        {
            // Сообщаем, что комната была очищена от врагов.
        }
    }
}
