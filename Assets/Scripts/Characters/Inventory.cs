using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public delegate void OnInventorySlotsChange(ItemSlot slot);

    public event OnInventorySlotsChange OnInventoryChange = delegate { };

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
        }
        else
        {
            item.AddAmount();
        }

        OnInventoryChange.Invoke(itemSO.Slot);
    }

    public void UpdateItemAmount(Item item)
    {
        OnInventoryChange.Invoke(item.ItemSO.Slot);
    }

    public void RemoveItem(int index, ItemSlot slot)
    {
        GetInventorySlots(slot).RemoveAt(index);
        OnInventoryChange.Invoke(slot);
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
