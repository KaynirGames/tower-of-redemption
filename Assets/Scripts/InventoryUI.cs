using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryScreen = null;

    public void ToggleInventory()
    {
        _inventoryScreen.SetActive(!_inventoryScreen.activeSelf);
        GameMaster.Instance.TogglePause();
    }
}
