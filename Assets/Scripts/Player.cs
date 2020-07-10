using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KaynirGames.Movement;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerSpec _currentSpec = null;

    private CharacterStats _playerStats; // Статы персонажа.

    private Vector2 _moveDirection = Vector2.zero;
    private CharacterMoveBase _characterMoveBase;
    private BaseAnimation _baseAnimation;

    private void Awake()
    {
        _playerStats = GetComponent<CharacterStats>();
        _characterMoveBase = GetComponent<CharacterMoveBase>();
        _baseAnimation = GetComponent<BaseAnimation>();
    }

    private void Start()
    {
        GameMaster.Instance.Player = this;
        _playerStats.SetStats(_currentSpec);
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetButtonDown("Attack"))
        {
            _baseAnimation.PlayAttackClip(1f, false);
        }
        else
        {
            _moveDirection.x = Input.GetAxisRaw("Horizontal");
            _moveDirection.y = Input.GetAxisRaw("Vertical");
            _characterMoveBase.SetMoveDirection(_moveDirection);
            _baseAnimation.PlayMoveClip(_moveDirection);
        }
    }
}
