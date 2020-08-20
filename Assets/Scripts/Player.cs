using KaynirGames.Movement;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static event Action<Player> OnPlayerActive = delegate { };
    public static event BattleManager.OnBattleEnd OnBattleEnd = delegate { };

    [SerializeField] private PlayerSpec _playerSpec = null;
    /// <summary>
    /// Специализация игрока.
    /// </summary>
    public PlayerSpec PlayerSpec => _playerSpec;
    /// <summary>
    /// Инвентарь игрока.
    /// </summary>
    public Inventory Inventory { get; private set; }
    /// <summary>
    /// Книга умений игрока.
    /// </summary>
    public SkillBook SkillBook { get; private set; }
    /// <summary>
    /// Статы персонажа.
    /// </summary>
    public CharacterStats PlayerStats { get; private set; }

    private Vector2 _moveDirection = Vector2.zero;
    private CharacterMoveBase _characterMoveBase;
    private BaseAnimation _baseAnimation;

    private void Awake()
    {
        PlayerStats = GetComponent<CharacterStats>();
        Inventory = GetComponent<Inventory>();
        SkillBook = GetComponent<SkillBook>();

        _characterMoveBase = GetComponent<CharacterMoveBase>();
        _baseAnimation = GetComponent<BaseAnimation>();

        PlayerStats.OnCharacterDeath += Die;
    }

    private void Start()
    {
        PlayerStats.SetBaseStats(_playerSpec);
        SkillBook.SetBaseSkills(_playerSpec);

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

    private void Die()
    {
        OnBattleEnd.Invoke(true);
    }
}
