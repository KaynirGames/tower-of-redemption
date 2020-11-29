using UnityEngine;

public class AssetManager : MonoBehaviour
{
    public static AssetManager Instance { get; private set; }

    [Header("Префабы:")]
    [SerializeField] private ItemSlotUI _itemSlotPrefab = null;
    [SerializeField] private GemstoneUI _gemSlotPrefab = null;
    [SerializeField] private FloatingTextPopup _spiritTextPopup = null;
    [SerializeField] private FloatingTextPopup _damageTextPopup = null;
    [Header("Базы данных:")]
    [SerializeField] private StatDatabase _statDatabase = null;
    [SerializeField] private DungeonStageDatabase _stageDatabase = null;

    public ItemSlotUI ItemSlotPrefab => _itemSlotPrefab;
    public GemstoneUI GemSlotPrefab => _gemSlotPrefab;
    public FloatingTextPopup SpiritTextPopup => _spiritTextPopup;
    public FloatingTextPopup DamageTextPopup => _damageTextPopup;

    public StatDatabase StatDatabase => _statDatabase;
    public DungeonStageDatabase StageDatabase => _stageDatabase;

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
