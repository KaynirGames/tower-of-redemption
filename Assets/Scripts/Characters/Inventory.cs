using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public delegate void OnItemSlotChange(Item item, ItemSlot slot, int index);

    public event OnItemSlotChange OnItemChange = delegate { };

    private List<Item> _consumables;
    private List<Item> _storyItems;

    private Dictionary<ItemSlot, List<Item>> _inventorySlots;

    private void Awake()
    {
        CreateInventory();
    }

    public void AddItem(ItemSO itemSO)
    {
        List<Item> items = GetInventorySlots(itemSO.Slot);
        Item item = items.Find(x => x.ItemSO == itemSO);

        if (item == null)
        {
            item = new Item(itemSO);
            items.Add(item);

            OnItemChange.Invoke(item, itemSO.Slot, items.Count - 1);
        }
        else
        {
            item.AddAmount();
            OnItemChange.Invoke(item, itemSO.Slot, items.IndexOf(item));
        }
    }

    public void RemoveItem(int index, ItemSlot slot)
    {
        GetInventorySlots(slot).RemoveAt(index);
        OnItemChange.Invoke(null, slot, index);
    }

    public void RemoveItem(Item item)
    {
        RemoveItem(GetInventorySlots(item.ItemSO.Slot).IndexOf(item),
                   item.ItemSO.Slot);
    }

    public List<Item> GetInventorySlots(ItemSlot slot)
    {
        return _inventorySlots[slot];
    }

    private void CreateInventory()
    {
        _consumables = new List<Item>();
        _storyItems = new List<Item>();

        _inventorySlots = new Dictionary<ItemSlot, List<Item>>()
        {
            { ItemSlot.Consumable, _consumables },
            { ItemSlot.KeyItem, _storyItems }
        };
    }
}
