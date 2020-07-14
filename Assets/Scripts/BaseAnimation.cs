using KaynirGames.Movement;
using System.Collections;
using UnityEngine;

/// <summary>
/// Основная анимация персонажа.
/// </summary>
public class BaseAnimation : MonoBehaviour
{
    [Header("Основные параметры аниматора:")]
    [SerializeField] private string _horizontalMoveParam = "Horizontal"; // Отвечает за движение по горизонтали.
    [SerializeField] private string _verticalMoveParam = "Vertical"; // Отвечает за движение по вертикали.
    [SerializeField] private string _moveSpeedParam = "Speed"; // Отвечает за скорость движения.
    [SerializeField] private string _attackParam = "Attack"; // Отвечает за триггер атаки.

    [Header("Необходимые компоненты:")]
    [SerializeField] private Animator _animator = null; // Аниматор персонажа.
    [SerializeField] private CharacterMoveBase _characterMoveMethod = null; // Метод движения персонажа.

    private bool _canAttack = true; // Доступность атаки в настоящий момент.

    /// <summary>
    /// Воспроизвести анимацию движения.
    /// </summary>
    public void PlayMoveClip(Vector2 moveDirection)
    {
        if (moveDirection != Vector2.zero)
        {
            _animator.SetFloat(_horizontalMoveParam, moveDirection.x);
            _animator.SetFloat(_verticalMoveParam, moveDirection.y);
        }
        _animator.SetFloat(_moveSpeedParam, moveDirection.sqrMagnitude);
    }
    /// <summary>
    /// Воспроизвести анимацию атаки.
    /// </summary>
    /// <param name="attackDelay">Задержка между атаками.</param>
    /// <param name="canMove">Возможность двигаться во время атаки.</param>
    public void PlayAttackClip(float attackDelay, bool canMove)
    {
        if (_canAttack)
        {
            _animator.SetTrigger(_attackParam);
            StartCoroutine(AttackRoutine(attackDelay, canMove));
        }
    }
    /// <summary>
    /// Корутина атаки персонажа.
    /// </summary>
    private IEnumerator AttackRoutine(float attackDelay, bool canMove)
    {
        _canAttack = false;
        if (!canMove) _characterMoveMethod.Disable();
        yield return new WaitForSeconds(attackDelay);
        _canAttack = true;
        if (!canMove) _characterMoveMethod.Enable();
    }
}