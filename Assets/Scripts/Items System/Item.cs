using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public ItemSO ItemSO { get; private set; }
    public int Amount { get; private set; }

    public Item(ItemSO itemSO)
    {
        ItemSO = itemSO;
        Amount = 1;
    }

    public void AddAmount()
    {
        Amount += 1;
    }

    public void Use(Character owner)
    {
        ItemSO.Use(owner);
        Amount--;
    }
}
