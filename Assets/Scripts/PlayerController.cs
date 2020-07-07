using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 35f; // Скорость перемещения.
    [SerializeField, Range(0f, 0.3f)] private float _moveSmoothing = 0.05f; // Величина сглаживания перемещения.

    private Vector2 _moveDirection = Vector2.zero; // Направление перемещения.
    private Vector2 _currentVelocity = Vector2.zero; // Модифицируется функцией сглаживания.

    private Rigidbody2D _playerRigidbody;
    private BaseAnimation _animation;

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _animation = GetComponent<BaseAnimation>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Attack"))
        {
            _animation.PlayAttackClip(.75f);
        }
        else
        {
            _moveDirection.x = Input.GetAxisRaw("Horizontal");
            _moveDirection.y = Input.GetAxisRaw("Vertical");
            _animation.PlayMoveClip(_moveDirection);
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    /// <summary>
    /// Осуществить перемещение персонажа.
    /// </summary>
    private void HandleMovement()
    {
        Vector2 targetVelocity = _moveDirection.normalized * _moveSpeed * Time.fixedDeltaTime;
        _playerRigidbody.velocity = Vector2.SmoothDamp(_playerRigidbody.velocity, targetVelocity * 10f, ref _currentVelocity, _moveSmoothing);
    }
}
