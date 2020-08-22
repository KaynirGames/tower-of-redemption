using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    [SerializeField] GameObject _battleWindow = null; // Окно боевой системы.
    [Header("Отображаемые данные об игроке:")]
    [SerializeField] private TextMeshProUGUI _playerNameDisplay = null;
    [SerializeField] private Image _playerGFXDisplay = null;
    [SerializeField] private ResourceDisplayUI _playerResourceDisplay = null;
    [SerializeField] private SkillDisplayUI _playerSkillDisplay = null;
    [Header("Отображаемые данные о враге:")]
    [SerializeField] private TextMeshProUGUI _enemyNameDisplay = null;
    [SerializeField] private Image _enemyGFXDisplay = null;
    [SerializeField] private ResourceDisplayUI _enemyResourceDisplay = null;
    [SerializeField] private StatDisplayUI _enemyStatsDisplay = null;
    [SerializeField] private EfficacyDisplayUI _enemyEfficacyDisplay = null;
    [SerializeField] private SkillDisplayUI _enemySkillDisplay = null;

    private Player _player;
    /// <summary>
    /// Вызвать окно боевой системы.
    /// </summary>
    public void ShowBattleWindow(Player player, Enemy enemy)
    {
        _battleWindow.SetActive(true);
        if (_player == null) { InitPlayer(player); }
        InitEnemy(enemy);
    }
    /// <summary>
    /// Закрыть окно боевой системы.
    /// </summary>
    public void CloseBattleWindow()
    {
        _battleWindow.SetActive(false);
    }

    private void InitPlayer(Player player)
    {
        _player = player;

        _playerNameDisplay.SetText(player.PlayerSpec.SpecName);
        // Графика
        _playerResourceDisplay.Init(player.PlayerStats);
        _playerSkillDisplay.Init(player.SkillBook);
    }

    private void InitEnemy(Enemy enemy)
    {
        _enemyNameDisplay.SetText(enemy.EnemySpec.Name);
        // Графика
        _enemyResourceDisplay.Init(enemy.EnemyStats);
        _enemyStatsDisplay.Init(enemy.EnemyStats);
        _enemyEfficacyDisplay.Init(enemy.EnemyStats);
        _enemySkillDisplay.Init(enemy.SkillBook);
    }
}
