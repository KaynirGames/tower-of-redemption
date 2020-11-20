using System.Collections.Generic;
using UnityEngine;

public class ItemContainerUI : MonoBehaviour
{
    [SerializeField] private int _maxSlots = 16;
    [SerializeField] private GameObject _slotsParent = null;

    private List<ItemSlotUI> _itemSlots = new List<ItemSlotUI>();

    private void Start()
    {
        CreateItemSlots();
    }

    public void RegisterItems(List<Item> items)
    {

    }

    private void CreateItemSlots()
    {
        _itemSlots.Clear();

        for (int i = 0; i < _maxSlots; i++)
        {
            GameObject slotObject = PoolManager.Instance.Take(AssetManager.Instance.itemSlotPrefab.tag);
            slotObject.transform.SetParent(_slotsParent.transform, false);

            _itemSlots.Add(slotObject.GetComponent<ItemSlotUI>());
        }
    }
}
