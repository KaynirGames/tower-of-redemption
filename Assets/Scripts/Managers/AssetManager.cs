using UnityEngine;

public class AssetManager : MonoBehaviour
{
    public static AssetManager Instance { get; private set; }

    [Header("Префабы:")]
    [SerializeField] private ItemSlotUI _itemSlotPrefab = null;
    [Header("Базы данных:")]
    [SerializeField] private StatDatabase _statDatabase = null;

    public ItemSlotUI itemSlotPrefab => _itemSlotPrefab;

    public StatDatabase StatDatabase => _statDatabase;

    private void Awake()
    {
        Instance = this;
    }
}
