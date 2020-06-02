using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField, Range(0f, 0.3f)] private float moveSmoothing = 0.05f; // Величина сглаживания перемещения.

    private Vector3 currentVelocity = Vector3.zero; // Модифицируется функцией сглаживания.

    /// <summary>
    /// Перемещение персонажа с учетом физики (желательно вызывать в FixedUpdate).
    /// </summary>
    /// <param name="moveX">Перемещение по оси X.</param>
    /// <param name="moveY">Перемещение по оси Y.</param>
    public void Move(float moveX, float moveY, Rigidbody2D rb)
    {
        Vector3 targetVelocity = new Vector3(moveX * 10f, moveY * 10f);

        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref currentVelocity, moveSmoothing);
    }
    /// <summary>
    /// Перемещение персонажа без учета физики.
    /// </summary>
    /// <param name="moveX"></param>
    /// <param name="moveY"></param>
    public void Move(float moveX, float moveY)
    {
        Vector3 newPos = new Vector3(moveX, moveY, 0);

        transform.Translate(newPos);
    }
}
