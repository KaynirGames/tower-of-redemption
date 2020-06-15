using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float runSpeed = 30f; // Скорость бега.
    [SerializeField] private CharacterStats characterStats = null; // Статы персонажа.
    [SerializeField] private PlayerSpec currentSpec = null;
    [SerializeField] private PlayerRuntimeSet activePlayer = null; // Набор, содержащий активного игрока.

    private Vector2 moveDirection; // Направление перемещения.

    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        characterStats.SetCharacterStats(currentSpec);
    }

    private void Update()
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        Vector2 targetVelocity = moveDirection.normalized * runSpeed * Time.fixedDeltaTime;
        playerController.HandleMovement(targetVelocity);
    }

    private void OnEnable()
    {
        activePlayer.Add(this);
    }

    private void OnDisable()
    {
        activePlayer.Remove(this);
    }
}
