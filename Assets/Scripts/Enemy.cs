using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float fieldAttackDistance = 2f;
    [SerializeField] private EnemySpec enemySpec = null;
    [SerializeField] private CharacterStats characterStats = null;
    [SerializeField] private PlayerRuntimeSet activePlayer = null;

    private Transform target;

    private void Start()
    {
        characterStats.SetCharacterStats(enemySpec);
        characterStats.OnCharacterDeath += Die;
        target = activePlayer.GetObject(0).transform;
    }

    private void Update()
    {
        if (target != null)
            FollowTarget(target);
    }

    private void FollowTarget(Transform target)
    {
        if (Vector2.Distance(transform.position, target.position) > fieldAttackDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, runSpeed * Time.deltaTime);
        }
        else
        {
            // Атаковать игрока.
        }
    }

    public void PerformFieldAttack()
    {
        // Атаковать игрока
    }

    private void Die()
    {

    }
}
