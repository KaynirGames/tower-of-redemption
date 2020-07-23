using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryScreen = null;

    private Inventory _inventory;

    private void Awake()
    {
        Player.OnPlayerActive += SetInventory;
    }

    public void ToggleInventory()
    {
        _inventoryScreen.SetActive(!_inventoryScreen.activeSelf);
        GameMaster.Instance.TogglePause();
    }

    private void SetInventory(Player player)
    {
        _inventory = player.Inventory;
    }
}