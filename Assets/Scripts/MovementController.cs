using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 30f; // Скорость перемещения.
    [SerializeField, Range(0f, 0.3f)] private float moveSmoothing = 0.05f; // Величина сглаживания перемещения.

    private Vector2 currentVelocity = Vector2.zero; // Модифицируется функцией сглаживания.

    /// <summary>
    /// Перемещение персонажа с учетом физики (желательно вызывать в FixedUpdate).
    /// </summary>
    /// <param name="moveX">Перемещение по оси X.</param>
    /// <param name="moveY">Перемещение по оси Y.</param>
    public void Move(float moveX, float moveY, Rigidbody2D rb)
    {
        Vector2 targetVelocity = new Vector2(moveX * moveSpeed * 10f, moveY * moveSpeed * 10f);

        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref currentVelocity, moveSmoothing);
    }
    /// <summary>
    /// Перемещение персонажа без учета физики.
    /// </summary>
    /// <param name="moveX"></param>
    /// <param name="moveY"></param>
    public void Move(float moveX, float moveY)
    {
        Vector2 newPos = new Vector2(moveX, moveY);

        transform.Translate(newPos);
    }
}
