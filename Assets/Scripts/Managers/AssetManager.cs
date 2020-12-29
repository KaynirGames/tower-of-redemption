using UnityEngine;

public class AssetManager : MonoBehaviour
{
    public static AssetManager Instance { get; private set; }

    [Header("Префабы:")]
    [SerializeField] private ItemSlotUI _itemSlotPrefab = null;
    [SerializeField] private GemstoneUI _gemSlotPrefab = null;
    [SerializeField] private TextPopup _spiritTextPopup = null;
    [SerializeField] private TextPopup _damageTextPopup = null;
    [SerializeField] private TextPopup _pickupItemPopup = null;
    [SerializeField] private TextPopup _dungeonTitlePopup = null;
    [SerializeField] private TextPopup _spiritShortageTextPopup = null;
    [Header("Базы данных:")]
    [SerializeField] private StatDatabase _statDatabase = null;
    [SerializeField] private DungeonStageDatabase _stageDatabase = null;
    [SerializeField] private PlayerDatabase _playerDatabase = null;
    [SerializeField] private SkillDatabase _skillDatabase = null;
    [SerializeField] private ItemDatabase _itemDatabase = null;

    public ItemSlotUI ItemSlotPrefab => _itemSlotPrefab;
    public GemstoneUI GemSlotPrefab => _gemSlotPrefab;
    public TextPopup SpiritTextPopup => _spiritTextPopup;
    public TextPopup DamageTextPopup => _damageTextPopup;
    public TextPopup PickupItemPopup => _pickupItemPopup;
    public TextPopup DungeonTitlePopup => _dungeonTitlePopup;
    public TextPopup SpiritShortageTextPopup => _spiritShortageTextPopup;

    public StatDatabase StatDatabase => _statDatabase;
    public DungeonStageDatabase StageDatabase => _stageDatabase;
    public PlayerDatabase PlayerDatabase => _playerDatabase;
    public SkillDatabase SkillDatabase => _skillDatabase;
    public ItemDatabase ItemDatabase => _itemDatabase;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
