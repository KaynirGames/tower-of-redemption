using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    [SerializeField] GameObject _battleWindow = null; // Окно боевой системы.
    [SerializeField] GameObject _playerUI = null;
    [SerializeField] GameObject _inventoryUI = null;
    [Header("Отображаемые данные об игроке:")]
    [SerializeField] private TextMeshProUGUI _playerNameDisplay = null;
    [SerializeField] private Image _playerGFXDisplay = null;
    [SerializeField] private ResourceDisplayUI _playerResourceDisplay = null;
    [SerializeField] private SkillBookUI _playerSkillDisplay = null;
    [Header("Отображаемые данные о враге:")]
    [SerializeField] private TextMeshProUGUI _enemyNameDisplay = null;
    [SerializeField] private Image _enemyGFXDisplay = null;
    [SerializeField] private ResourceDisplayUI _enemyResourceDisplay = null;
    [SerializeField] private StatDisplayUI _enemyStatsDisplay = null;
    [SerializeField] private EfficacyDisplayUI _enemyEfficacyDisplay = null;
    [SerializeField] private SkillBookUI _enemySkillDisplay = null;

    private PlayerCharacter _player;
    /// <summary>
    /// Вызвать окно боевой системы.
    /// </summary>
    public void ShowBattleWindow(PlayerCharacter player, EnemyCharacter enemy)
    {
        _playerUI.SetActive(false);
        _inventoryUI.SetActive(false);
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
        _playerUI.SetActive(true);
        _inventoryUI.SetActive(true);
    }

    private void InitPlayer(PlayerCharacter player)
    {
        _player = player;

        _playerNameDisplay.SetText(player.PlayerSpec.SpecName);
        // Графика
        _playerResourceDisplay.RegisterResources(player.Stats);
        _playerSkillDisplay.Init(player.SkillBook);
    }

    private void InitEnemy(EnemyCharacter enemy)
    {
        _enemyNameDisplay.SetText(enemy.EnemySpec.SpecName);
        // Графика
        _enemyResourceDisplay.RegisterResources(enemy.Stats);
        _enemyStatsDisplay.RegisterStats(enemy.Stats);
        _enemyEfficacyDisplay.RegisterElementEfficacies(enemy.Stats);
        _enemySkillDisplay.Init(enemy.SkillBook);
    }
}
