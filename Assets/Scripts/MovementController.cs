using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour
{
    [SerializeField, Range(0f, 0.3f)] private float moveSmoothing = 0.05f; // Величина сглаживания перемещения.

    private Rigidbody2D rb;

    private Vector3 currentVelocity = Vector3.zero; // Модифицируется функцией сглаживания.

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Перемещение персонажа в заданном направлении.
    /// </summary>
    /// <param name="moveX">Перемещение по оси X.</param>
    /// <param name="moveY">Перемещение по оси Y.</param>
    public void Move(float moveX, float moveY)
    {
        Vector3 targetVelocity = new Vector3(moveX * 10f, moveY * 10f);

        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref currentVelocity, moveSmoothing);
    }
}
