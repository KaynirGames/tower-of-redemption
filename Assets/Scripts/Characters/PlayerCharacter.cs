using KaynirGames.Movement;
using System;
using System.Collections;
using UnityEngine;
using KaynirGames.InputSystem;

public class PlayerCharacter : Character
{
    public static event Action<PlayerCharacter> OnPlayerActive = delegate { };
    public static event BattleManager.OnBattleEnd OnBattleEnd = delegate { };

    public static PlayerCharacter Active { get; private set; }

    [SerializeField] private PlayerSpec _playerSpec = null;
    [SerializeField] private float _attackSpeed = 1f;
    [SerializeField] private InputHandler _inputHandler = null;

    public PlayerSpec PlayerSpec => _playerSpec;

    public Inventory Inventory { get; private set; }

    private MoveBase _moveBase;
    private GameMaster _gameMaster;
    private WaitForSeconds _waitForNextAttack;

    private bool _enableAttack;
    private bool _enableInput;

    protected override void Awake()
    {
        base.Awake();

        _moveBase = GetComponent<MoveBase>();
        Inventory = GetComponent<Inventory>();

        _enableAttack = true;
        _enableInput = true;
        _waitForNextAttack = new WaitForSeconds(_attackSpeed);

        Stats.OnCharacterDeath += Die;
    }

    private void Start()
    {
        _gameMaster = GameMaster.Instance;

        Stats.SetCharacterStats(_playerSpec);
        SkillBook.SetBaseSkills(_playerSpec);

        Active = this;
        OnPlayerActive.Invoke(this);
    }

    private void Update()
    {
        if (_gameMaster.IsPause)
        {
            Animations.PlayMoveClip(Vector2.zero);
            return;
        }

        if (_enableInput)
        {
            HandleInput();
        }
    }

    public override void PrepareForBattle()
    {
        Animations.PlayMoveClip(Vector2.right);
    }

    public void ToggleInput(bool enabled)
    {
        if (!enabled)
        {
            _moveBase.SetMoveDirection(Vector3.zero);
        }

        _enableInput = enabled;
    }

    public override void ExitBattle(Vector3 lastPosition)
    {
        base.ExitBattle(lastPosition);

        Effects.DisableBattleEffects();
        SkillBook.ResetSkillCooldowns();
    }

    private void HandleInput()
    {
        if (_inputHandler.GetAttackInput())
        {
            if (_enableAttack)
            {
                StartCoroutine(AttackRoutine());
            }
        }
        else
        {
            Vector2 moveDirection = _inputHandler.GetMovementInput();
            _moveBase.SetMoveDirection(moveDirection);
            Animations.PlayMoveClip(moveDirection);
        }
    }

    protected override void Die()
    {
        Animations.PlayDeathClip();
        OnBattleEnd.Invoke(false);
    }

    private IEnumerator AttackRoutine()
    {
        _enableAttack = false;

        Animations.PlayAttackClip();

        yield return _waitForNextAttack;

        _enableAttack = true;
    }
}