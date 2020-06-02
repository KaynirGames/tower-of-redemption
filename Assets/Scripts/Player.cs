using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 30f; // Скорость перемещения игрока.

    [SerializeField] private MovementController movementController = null;

    private float moveX; // Перемещение по оси X.
    private float moveY; // Перемещение по оси Y.

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveX = Input.GetAxis("Horizontal") * moveSpeed;
        moveY = Input.GetAxis("Vertical") * moveSpeed;
    }

    private void FixedUpdate()
    {
        movementController.Move(moveX * Time.fixedDeltaTime, moveY * Time.fixedDeltaTime, rb);
    }
}
