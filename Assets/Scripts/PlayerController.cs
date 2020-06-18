using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 35f; // Скорость перемещения.
    [SerializeField, Range(0f, 0.3f)] private float moveSmoothing = 0.05f; // Величина сглаживания перемещения.

    private Vector2 moveDirection; // Направление перемещения.
    private Vector2 currentVelocity = Vector2.zero; // Модифицируется функцией сглаживания.

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        Vector2 targetVelocity = moveDirection.normalized * moveSpeed * Time.fixedDeltaTime;
        HandleMovement(targetVelocity);
    }
    /// <summary>
    /// Обработать нажатие клавиш управления персонажем.
    /// </summary>
    private void HandleInput()
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
    /// <summary>
    /// Осуществить перемещение персонажа.
    /// </summary>
    /// <param name="targetVelocity">Желаемая величина перемещения.</param>
    private void HandleMovement(Vector2 targetVelocity)
    {
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity * 10f, ref currentVelocity, moveSmoothing);
    }
}
