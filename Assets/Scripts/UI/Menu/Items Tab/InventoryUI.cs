using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private Inventory _inventory;

    public void RegisterInventory(Inventory inventory)
    {
        _inventory = inventory;
    }


}
