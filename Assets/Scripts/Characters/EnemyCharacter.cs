using KaynirGames.Movement;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    public static event BattleManager.OnBattleTrigger OnBattleTrigger = delegate { return false; };
    public static event BattleManager.OnBattleEnd OnBattleEnd = delegate { };

    public static List<EnemyCharacter> ActiveEnemies = new List<EnemyCharacter>();

    [SerializeField] private EnemySpec _enemySpec = null;

    public EnemySpec EnemySpec => _enemySpec;
    public bool IsMoving => _movePositionBase.IsMoving;

    private MovePositionBase _movePositionBase;
    private MoveBase _moveBase;
    private EnemyAI _enemyAI = null;
    private EnemyBattleAI _enemyBattleAI = null;

    protected override void Awake()
    {
        base.Awake();

        _moveBase = GetComponent<MoveBase>();
        _movePositionBase = GetComponent<MovePositionBase>();
        _enemyAI = GetComponent<EnemyAI>();
        _enemyBattleAI = GetComponent<EnemyBattleAI>();

        Stats.OnCharacterDeath += Die;
    }

    private void Start()
    {
        Stats.SetCharacterStats(_enemySpec);
        SkillBook.LoadSkills(_enemySpec.BaseSkills);

        _enemyBattleAI.InitializeBattleAI();
        ActiveEnemies.Add(this);
    }

    public void HandleMovement(Vector3 position)
    {
        Vector2 moveDirection = (position - transform.position).normalized;
        _movePositionBase.SetPosition(position);
        PlayMoveAnimation(moveDirection);
    }

    public void StopMovement()
    {
        _movePositionBase.StopMovement();
        _moveBase.SetMoveDirection(Vector3.zero);
        PlayMoveAnimation(Vector2.zero);
    }

    public override void PrepareForBattle()
    {
        _enemyAI.enabled = false;
        PlayMoveAnimation(Vector2.left);
        StopMovement();
        ToggleBattleAI(true);
    }

    public override void ExitBattle(Vector3 lastPosition)
    {
        base.ExitBattle(lastPosition);
        StopAllCoroutines();
        ToggleBattleAI(false);
        Destroy(gameObject);
    }

    private void ToggleBattleAI(bool enable)
    {
        _enemyBattleAI.enabled = enable;
        _enemyBattleAI.ToggleSpiritRegen(enable);
    }

    protected override void Die()
    {
        OnBattleEnd.Invoke(true);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.GetComponent<PlayerCharacter>() != null)
        {
            bool inBattle = OnBattleTrigger.Invoke(this, false);

            if (inBattle) { _enemyAI.enabled = false; }
        }
    }
}
