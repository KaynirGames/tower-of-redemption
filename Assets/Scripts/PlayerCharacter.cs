using KaynirGames.Movement;
using System;
using UnityEngine;

public class PlayerCharacter : Character
{
    public static event Action<PlayerCharacter> OnPlayerActive = delegate { };
    public static event BattleManager.OnBattleEnd OnBattleEnd = delegate { };

    [SerializeField] private PlayerSpec _playerSpec = null;

    public PlayerSpec PlayerSpec => _playerSpec;

    public Inventory Inventory { get; private set; }

    private Vector2 _moveDirection = Vector2.zero;
    private CharacterMoveBase _characterMoveBase;
    private BaseAnimation _baseAnimation;

    protected override void Awake()
    {
        base.Awake();

        _characterMoveBase = GetComponent<CharacterMoveBase>();
        _baseAnimation = GetComponent<BaseAnimation>();

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

    protected override void Die()
    {
        OnBattleEnd.Invoke(true);
    }
}
