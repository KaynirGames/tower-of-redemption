using UnityEngine;

public class AssetManager : MonoBehaviour
{
    public static AssetManager Instance { get; private set; }

    [Header("Префабы:")]
    [SerializeField] private GemstoneUI _gemSlotPrefab = null;
    [Header("Базы данных:")]
    [SerializeField] private StatDatabase _statDatabase = null;
    [SerializeField] private DungeonStageDatabase _stageDatabase = null;
    [SerializeField] private PlayerDatabase _playerDatabase = null;
    [SerializeField] private SkillDatabase _skillDatabase = null;
    [SerializeField] private ItemDatabase _itemDatabase = null;

    public GemstoneUI GemSlotPrefab => _gemSlotPrefab;

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
