using KaynirGames.Movement;
using System;
using System.Collections;
using UnityEngine;
using KaynirGames.InputSystem;

public class PlayerCharacter : Character
{
    public static event Action<PlayerCharacter> OnPlayerActive = delegate { };
    public static event BattleManager.OnBattleEnd OnBattleEnd = delegate { };

    [SerializeField] private PlayerSpec _playerSpec = null;
    [SerializeField] private float _attackSpeed = 1f;
    [SerializeField] private bool _allowMovementOnAttack = false;
    [SerializeField] private InputHandler _inputHandler = null;

    public PlayerSpec PlayerSpec => _playerSpec;

    public Inventory Inventory { get; private set; }

    private Vector2 _moveDirection = Vector2.zero;
    private CharacterMoveBase _characterMoveBase;
    private BaseAnimation _baseAnimation;

    private bool _isAttackAvailable;
    private WaitForSeconds _waitForNextAttack;

    protected override void Awake()
    {
        base.Awake();

        _characterMoveBase = GetComponent<CharacterMoveBase>();
        _baseAnimation = GetComponentInChildren<BaseAnimation>();

        _isAttackAvailable = true;
        _waitForNextAttack = new WaitForSeconds(_attackSpeed);

        Stats.OnCharacterDeath += Die;
    }

    private void Start()
    {
        Stats.SetCharacterStats(_playerSpec);
        SkillBook.SetBaseSpecSkills(_playerSpec);

        OnPlayerActive.Invoke(this);
    }

    private void Update()
    {
        if (GameMaster.Instance.IsPause) return;

        HandleInput();
    }

    public override void ToggleMovement(bool enable)
    {
        if (enable)
        {
            _characterMoveBase.Enable();
        }
        else
        {
            _characterMoveBase.Disable();
        }
    }

    private void HandleInput()
    {
        if (_inputHandler.GetAttackInput())
        {
            if (_isAttackAvailable)
            {
                StartCoroutine(AttackRoutine(_allowMovementOnAttack));
            }
        }
        else
        {
            _moveDirection = _inputHandler.GetMovementInput();
            _characterMoveBase.SetMoveDirection(_moveDirection);
            _baseAnimation.PlayMoveClip(_moveDirection);
        }
    }

    protected override void Die()
    {
        OnBattleEnd.Invoke(true);
    }

    private IEnumerator AttackRoutine(bool allowMovement)
    {
        _isAttackAvailable = false;

        _baseAnimation.PlayAttackClip();

        if (!allowMovement)
        {
            ToggleMovement(false);
        }

        yield return _waitForNextAttack;

        _isAttackAvailable = true;
    }
}