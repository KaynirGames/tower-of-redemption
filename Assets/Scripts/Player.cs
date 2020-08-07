using KaynirGames.Movement;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static event Action<Player> OnPlayerActive = delegate { };

    [SerializeField] private PlayerSpec _currentSpec = null;
    [SerializeField] private Inventory _inventory = null;
    [SerializeField] private SkillBook _skillBook = null;
    /// <summary>
    /// Специализация игрока.
    /// </summary>
    public PlayerSpec PlayerSpec => _currentSpec;
    /// <summary>
    /// Инвентарь игрока.
    /// </summary>
    public Inventory Inventory => _inventory;
    /// <summary>
    /// Книга умений игрока.
    /// </summary>
    public SkillBook SkillBook => _skillBook;

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
        _playerStats.SetBaseStats(_currentSpec);
        _skillBook.SetBaseSkills(_currentSpec);

        OnPlayerActive?.Invoke(this);
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
}
