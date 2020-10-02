using TMPro;
using UnityEngine;

public class BattleUI : MonoBehaviour
{
    [SerializeField] Canvas _playerHUDCanvas = null;
    [Header("Отображаемые данные об игроке:")]
    [SerializeField] private ResourceDisplayUI _playerResourceDisplay = null;
    [SerializeField] private SkillBookUI _playerSkillDisplay = null;
    [Header("Отображаемые данные о враге:")]
    [SerializeField] private TextMeshProUGUI _enemyNameDisplay = null;
    [SerializeField] private ResourceDisplayUI _enemyResourceDisplay = null;
    [SerializeField] private StatDisplayUI _enemyStatsDisplay = null;
    [SerializeField] private EfficacyDisplayUI _enemyEfficacyDisplay = null;
    [SerializeField] private SkillBookUI _enemySkillDisplay = null;

    private Canvas _battleCanvas;
    private PlayerCharacter _player;

    private void Awake()
    {
        _battleCanvas = GetComponent<Canvas>();
    }

    public void ShowBattleWindow(PlayerCharacter player, EnemyCharacter enemy)
    {
        if (_player == null) { InitPlayer(player); }
        InitEnemy(enemy);

        _playerHUDCanvas.enabled = false;
        _battleCanvas.enabled = true;
    }

    public void CloseBattleWindow()
    {
        _battleCanvas.enabled = false;
        _playerHUDCanvas.enabled = true;

        ClearEnemy();
    }

    private void InitPlayer(PlayerCharacter player)
    {
        _player = player;

        _playerResourceDisplay.RegisterResources(player.Stats);
        _playerSkillDisplay.RegisterSkillBook(player.SkillBook);
    }

    private void InitEnemy(EnemyCharacter enemy)
    {
        _enemyNameDisplay.SetText(enemy.EnemySpec.SpecName);
        _enemyResourceDisplay.RegisterResources(enemy.Stats);
        _enemyStatsDisplay.RegisterStats(enemy.Stats);
        _enemyEfficacyDisplay.RegisterElementEfficacies(enemy.Stats);
        _enemySkillDisplay.RegisterSkillBook(enemy.SkillBook);
    }

    private void ClearEnemy()
    {
        _enemyNameDisplay.ClearMesh();
        _enemyResourceDisplay.Clear();
        _enemyStatsDisplay.Clear();
        _enemyEfficacyDisplay.Clear();
        _enemySkillDisplay.Clear();
    }
}
