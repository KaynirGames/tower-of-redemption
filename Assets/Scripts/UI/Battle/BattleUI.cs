using TMPro;
using UnityEngine;

public class BattleUI : MonoBehaviour
{
    [SerializeField] PlayerUI _playerUI = null;
    [Header("Параметры игрока:")]
    [SerializeField] private RectTransform _playerPlacement = null;
    [SerializeField] private ResourceDisplayUI _playerResourceDisplay = null;
    [SerializeField] private SkillBookUI _playerSkillDisplay = null;
    [SerializeField] private EffectDisplayUI _playerEffectDisplay = null;
    [Header("Параметры противника:")]
    [SerializeField] private RectTransform _enemyPlacement = null;
    [SerializeField] private TextMeshProUGUI _enemyNameDisplay = null;
    [SerializeField] private ResourceDisplayUI _enemyResourceDisplay = null;
    [SerializeField] private StatDisplayUI _enemyStatsDisplay = null;
    [SerializeField] private EfficacyDisplayUI _enemyEfficacyDisplay = null;
    [SerializeField] private SkillBookUI _enemySkillDisplay = null;
    [SerializeField] private EffectDisplayUI _enemyEffectDisplay = null;

    public RectTransform PlayerPlacement => _playerPlacement;
    public RectTransform EnemyPlacement => _enemyPlacement;

    private PlayerCharacter _player;

    private CanvasGroup _battleCanvasGroup;

    private void Awake()
    {
        _battleCanvasGroup = GetComponent<CanvasGroup>();
    }

    public void ShowBattleUI(PlayerCharacter player, EnemyCharacter enemy)
    {
        InitPlayer(player);
        InitEnemy(enemy);

        _playerUI.TogglePlayerHUD(false);
        ToggleBattleWindow(true);
    }

    public void CloseBattleUI()
    {
        ClearEnemyUI();

        ToggleBattleWindow(false);
        _playerUI.TogglePlayerHUD(true);
    }

    public void ToggleBattleWindow(bool enable)
    {
        _battleCanvasGroup.alpha = enable ? 1 : 0;
        _battleCanvasGroup.blocksRaycasts = enable;
    }

    private void InitPlayer(PlayerCharacter player)
    {
        if (_player == null)
        {
            _player = player;
            _playerResourceDisplay.RegisterResources(player.Stats);
            _playerSkillDisplay.RegisterSkillBook(player.SkillBook);
            _playerEffectDisplay.RegisterCharacterEffects(player.Effects);
        }
    }

    private void InitEnemy(EnemyCharacter enemy)
    {
        _enemyNameDisplay.SetText(enemy.EnemySpec.SpecName);
        _enemyResourceDisplay.RegisterResources(enemy.Stats);
        _enemyStatsDisplay.RegisterStats(enemy.Stats);
        _enemyEfficacyDisplay.RegisterElementEfficacies(enemy.Stats);
        _enemySkillDisplay.RegisterSkillBook(enemy.SkillBook);
        _enemyEffectDisplay.RegisterCharacterEffects(enemy.Effects);
    }

    private void ClearEnemyUI()
    {
        _enemyResourceDisplay.ClearResourceUI();
        _enemyStatsDisplay.ClearStatsUI();
        _enemyEfficacyDisplay.ClearEfficaciesUI();
        _enemySkillDisplay.ClearBookUI();
        _enemyEffectDisplay.ClearEffectsUI();
    }
}
