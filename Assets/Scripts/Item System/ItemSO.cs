using UnityEditor;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "Undefined Item SO", menuName = "Scriptable Objects/Items/Item SO")]
public class ItemSO : ScriptableObject, IIdentifiable
{
    [Header("Параметры отображения предмета:")]
    [SerializeField] private LocalizedString _name = null;
    [SerializeField] private LocalizedString _description = null;
    [SerializeField] private LocalizedString _type = null;
    [SerializeField] private ItemSlot _slot = ItemSlot.Consumable;
    [SerializeField] private Sprite _icon = null;

    public string Name => _name.GetLocalizedString().Result;
    public string Description => _description.GetLocalizedString().Result;
    public string Type => _type.GetLocalizedString().Result;
    public ItemSlot Slot => _slot;
    public bool CanUse => _slot == ItemSlot.Consumable;

    public virtual Sprite Icon => _icon;

    public string ID { get; private set; }

    public virtual bool TryUse(PlayerCharacter player, Item item)
    {
        return false;
    }

#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);
        ID = AssetDatabase.AssetPathToGUID(path);
    }
#endif
}