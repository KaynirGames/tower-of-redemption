using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController playerController;
    public float moveSpeed = 30f; // Скорость перемещения игрока.

    private float moveX; // Перемещение по оси X.
    private float moveY; // Перемещение по оси Y.

    private void Update()
    {
        moveX = Input.GetAxis("Horizontal") * moveSpeed;
        moveY = Input.GetAxis("Vertical") * moveSpeed;
    }

    private void FixedUpdate()
    {
        playerController.Move(moveX * Time.fixedDeltaTime, moveY * Time.fixedDeltaTime);
    }
}
