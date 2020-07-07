using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KaynirGames.Movement;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerSpec currentSpec = null;
    [SerializeField] private PlayerRuntimeSet activePlayerRS = null; // Набор, содержащий активного игрока.

    private CharacterStats playerStats; // Статы персонажа.

    private Vector2 _moveDirection = Vector2.zero;
    private CharacterMoveBase _characterMoveBase;
    private BaseAnimation _baseAnimation;

    private void Awake()
    {
        activePlayerRS.Add(this);
        playerStats = GetComponent<CharacterStats>();
        _characterMoveBase = GetComponent<CharacterMoveBase>();
        _baseAnimation = GetComponent<BaseAnimation>();
    }

    private void Start()
    {
        playerStats.SetStats(currentSpec);
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetButtonDown("Attack"))
        {
            _baseAnimation.PlayAttackClip(1f);
        }
        else
        {
            _moveDirection.x = Input.GetAxisRaw("Horizontal");
            _moveDirection.y = Input.GetAxisRaw("Vertical");
            _characterMoveBase.SetMoveDirection(_moveDirection);
            _baseAnimation.PlayMoveClip(_moveDirection);
        }
    }

    private void OnDisable()
    {
        activePlayerRS.Remove(this);
    }
}
