using TMPro;
using UnityEngine;

public class BattleUI : MonoBehaviour
{
    [SerializeField] PlayerUI _playerUI = null;
    [Header("Настройка игрока:")]
    [SerializeField] private RectTransform _playerPlacement = null;
    [SerializeField] private ResourceDisplayUI _playerResourceDisplay = null;
    [SerializeField] private SkillBookUI _playerSkillDisplay = null;
    [Header("Настройка противника:")]
    [SerializeField] private RectTransform _enemyPlacement = null;
    [SerializeField] private TextMeshProUGUI _enemyNameDisplay = null;
    [SerializeField] private ResourceDisplayUI _enemyResourceDisplay = null;
    [SerializeField] private StatDisplayUI _enemyStatsDisplay = null;
    [SerializeField] private EfficacyDisplayUI _enemyEfficacyDisplay = null;
    [SerializeField] private SkillBookUI _enemySkillDisplay = null;

    private PlayerCharacter _player;

    private CanvasGroup _battleCanvasGroup;

    private Camera _camera;

    private void Awake()
    {
        _battleCanvasGroup = GetComponent<CanvasGroup>();
        _camera = Camera.main;
    }

    public void ShowBattleUI(PlayerCharacter player, EnemyCharacter enemy)
    {
        if (_player == null) { InitPlayer(player); }
        InitEnemy(enemy);

        _playerUI.TogglePlayerHUD(false);
        ToggleBattleWindow(true);
    }

    public void CloseBattleUI()
    {
        ToggleBattleWindow(false);
        _playerUI.TogglePlayerHUD(true);

        ClearEnemy();
    }

    public void ToggleBattleWindow(bool enable)
    {
        _battleCanvasGroup.alpha = enable ? 1 : 0;
        _battleCanvasGroup.blocksRaycasts = enable;
    }

    private void InitPlayer(PlayerCharacter player)
    {
        _player = player;

        SetCharacterPosition(_player, _playerPlacement);

        _playerResourceDisplay.RegisterResources(player.Stats);
        _playerSkillDisplay.RegisterSkillBook(player.SkillBook);
    }

    private void InitEnemy(EnemyCharacter enemy)
    {
        SetCharacterPosition(enemy, _enemyPlacement);

        _enemyNameDisplay.SetText(enemy.EnemySpec.SpecName);
        _enemyResourceDisplay.RegisterResources(enemy.Stats);
        _enemyStatsDisplay.RegisterStats(enemy.Stats);
        _enemyEfficacyDisplay.RegisterElementEfficacies(enemy.Stats);
        _enemySkillDisplay.RegisterSkillBook(enemy.SkillBook);
    }

    private void ClearEnemy()
    {
        _enemyNameDisplay.SetText("");
        _enemyResourceDisplay.Clear();
        _enemyStatsDisplay.Clear();
        _enemyEfficacyDisplay.Clear();
        _enemySkillDisplay.Clear();
    }

    private void SetCharacterPosition(Character character, RectTransform placement)
    {
        Vector3 position = _camera.ScreenToWorldPoint(placement.position);
        position.z = 0;
        character.transform.position = position;
    }
}
