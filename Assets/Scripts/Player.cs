using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerSpec currentSpec = null;
    [SerializeField] private PlayerRuntimeSet activePlayerRS = null; // Набор, содержащий активного игрока.
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private Transform attackPoint = null;

    private CharacterStats playerStats; // Статы персонажа.

    private void Awake()
    {
        activePlayerRS.Add(this);
        playerStats = GetComponent<CharacterStats>();
    }

    private void Start()
    {
        playerStats.SetStats(currentSpec);
    }

    private void OnDisable()
    {
        activePlayerRS.Remove(this);
    }

    private void OnDrawGizmos()
    {
        if (attackRange > 0)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}
