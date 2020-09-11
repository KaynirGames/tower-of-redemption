using UnityEngine;

/// <summary>
/// Основная анимация персонажа.
/// </summary>
[RequireComponent(typeof(Animator))]
public class BaseAnimation : MonoBehaviour
{
    [SerializeField] private string _horizontalMoveParam = "Horizontal";
    [SerializeField] private string _verticalMoveParam = "Vertical";
    [SerializeField] private string _moveSpeedParam = "Speed";
    [SerializeField] private string _attackParam = "Attack";
    [SerializeField] private Character _characterForAnimation = null;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayMoveClip(Vector2 moveDirection)
    {
        if (moveDirection != Vector2.zero)
        {
            _animator.SetFloat(_horizontalMoveParam, moveDirection.x);
            _animator.SetFloat(_verticalMoveParam, moveDirection.y);
        }
        _animator.SetFloat(_moveSpeedParam, moveDirection.sqrMagnitude);
    }

    public void PlayAttackClip()
    {
        _animator.SetTrigger(_attackParam);
    }

    public void EnableMovementAfterAttackAnimation()
    {
        _characterForAnimation.ToggleMovement(true);
    }
}