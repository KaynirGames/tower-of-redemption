using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BaseAnimation : MonoBehaviour
{
    [SerializeField] private string _horizontalMoveParam = "Horizontal";
    [SerializeField] private string _verticalMoveParam = "Vertical";
    [SerializeField] private string _moveSpeedParam = "Speed";
    [SerializeField] private string _attackParam = "Attack";

    private Animator _animator;
    private bool _canAttack = true;

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

    public void PlayAttackClip(float attackDelay)
    {
        if (_canAttack)
        {
            _animator.SetTrigger(_attackParam);
            StartCoroutine(AttackRoutine(attackDelay));
        }
    }

    private IEnumerator AttackRoutine(float attackDelay)
    {
        _canAttack = false;
        yield return new WaitForSeconds(attackDelay);
        _canAttack = true;
    }
}