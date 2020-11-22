using System.Collections.Generic;
using UnityEngine;

public class ItemContainerUI : MonoBehaviour
{
    [SerializeField] private int _maxSlots = 16;
    [SerializeField] private GameObject _slotsParent = null;
    [SerializeField] private ItemSlot _containerType = ItemSlot.Consumable;

    private List<ItemSlotUI> _itemSlots = new List<ItemSlotUI>();

    private PoolManager _poolManager;
    private AssetManager _assetManager;

    private void Start()
    {
        _poolManager = PoolManager.Instance;
        _assetManager = AssetManager.Instance;
        CreateItemSlots();
    }

    public void RegisterItems(List<Item> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            _itemSlots[i].UpdateSlot(items[i]);
        }
    }

    public void UpdateContainerSlot(Item item, ItemSlot slotType, int index)
    {
        if (_containerType == slotType)
        {
            if (item == null)
            {
                HandleEmptiedSlot(index);
            }
            else
            {
                if (index > _maxSlots)
                {
                    CreateSlot();
                }

                _itemSlots[index].UpdateSlot(item);
            }
        }
    }

    private void HandleEmptiedSlot(int index)
    {
        ItemSlotUI empty = _itemSlots[index];

        _itemSlots.RemoveAt(index);
        empty.UpdateSlot(null);

        if (_itemSlots.Count < _maxSlots)
        {
            _itemSlots.Add(empty);
        }
        else
        {
            _poolManager.Store(empty.gameObject);
        }
    }

    private void CreateItemSlots()
    {
        _itemSlots.Clear();

        for (int i = 0; i < _maxSlots; i++)
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
}
