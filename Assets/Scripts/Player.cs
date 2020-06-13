﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private MovementController movementController = null;
    [SerializeField] private CharacterStats characterStats = null;

    private float moveX; // Перемещение по оси X.
    private float moveY; // Перемещение по оси Y.

    private Rigidbody2D rb;

    private StatModifier healthModifier;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        healthModifier = new StatModifier(5);
    }

    private void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        movementController.Move(moveX * Time.fixedDeltaTime, moveY * Time.fixedDeltaTime, rb);
    }
}
