using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 35f; // Скорость перемещения.
    [SerializeField, Range(0f, 0.3f)] private float moveSmoothing = 0.05f; // Величина сглаживания перемещения.

    private Vector2 moveDirection; // Направление перемещения.
    private Vector2 currentVelocity = Vector2.zero; // Модифицируется функцией сглаживания.

    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }
    /// <summary>
    /// Обработать нажатие клавиш управления персонажем.
    /// </summary>
    private void HandleInput()
    {
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.y = Input.GetAxis("Vertical");

        animator.SetFloat("Horizontal", moveDirection.x);
        animator.SetFloat("Vertical", moveDirection.y);
        animator.SetFloat("Speed", moveDirection.sqrMagnitude);
    }
    /// <summary>
    /// Осуществить перемещение персонажа.
    /// </summary>
    private void HandleMovement()
    {
        Vector2 targetVelocity = moveDirection.normalized * moveSpeed * Time.fixedDeltaTime;
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity * 10f, ref currentVelocity, moveSmoothing);
    }
}
