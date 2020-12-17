using System.Collections.Generic;
using UnityEngine;

public class ItemContainerUI : MonoBehaviour
{
    [SerializeField] private int _maxSlots = 16;
    [SerializeField] private GameObject _slotsParent = null;
    [SerializeField] private ItemSlot _containerType = ItemSlot.Consumable;

    private List<ItemSlotUI> _itemSlots;
    private List<Item> _items;

    private PoolManager _poolManager;
    private AssetManager _assetManager;

    private void Start()
    {
        _poolManager = PoolManager.Instance;
        _assetManager = AssetManager.Instance;
        _itemSlots = new List<ItemSlotUI>();

        CreateItemSlots(_maxSlots);
    }

    public void RegisterItems(List<Item> items)
    {
        _items = items;
        UpdateItemSlots();
    }

    public void UpdateContainer(ItemSlot slotType)
    {
        if (_containerType == slotType)
        {
            UpdateItemSlots();
        }
    }

    private void UpdateItemSlots()
    {
        if (_items.Count > _itemSlots.Count)
        {
            CreateItemSlots(_items.Count - _itemSlots.Count);
        }
        else if (_itemSlots.Count > _maxSlots)
        {
            RemoveItemSlots(_itemSlots.Count - _maxSlots);
        }

        for (int i = 0; i < _items.Count; i++)
        {
            _itemSlots[i].UpdateSlot(_items[i]);
        }

        for (int i = _items.Count; i < _itemSlots.Count; i++)
        {
            _itemSlots[i].UpdateSlot(null);
        }
    }

    private void CreateItemSlots(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            CreateSlot();
        }
    }

    private void CreateSlot()
    {
        GameObject slotObject = _poolManager.Take(_assetManager.ItemSlotPrefab.tag);
        slotObject.transform.SetParent(_slotsParent.transform, false);

        _itemSlots.Add(slotObject.GetComponent<ItemSlotUI>());
    }

    private void RemoveItemSlots(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            ItemSlotUI empty = _itemSlots[i];
            empty.UpdateSlot(null);

            _itemSlots.RemoveAt(i);
            _poolManager.Store(empty.gameObject);
        }
    }
}
