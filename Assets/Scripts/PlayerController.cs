using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(0f, 0.3f)] private float moveSmoothing = 0.05f; // Величина сглаживания перемещения.

    private Vector2 currentVelocity = Vector2.zero; // Модифицируется функцией сглаживания.
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    /// <summary>
    /// Осуществить перемещение персонажа (вызывается в FixedUpdate).
    /// </summary>
    /// <param name="targetVelocity">Желаемая величина перемещения.</param>
    public void HandleMovement(Vector2 targetVelocity)
    {
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity * 10f, ref currentVelocity, moveSmoothing);
    }
}
