using UnityEngine;

public class ItemsTabUI : MenuTabUI
{
    [SerializeField] private ItemContainerUI _consumables = null;
    [SerializeField] private ItemContainerUI _keyItems = null;

    public override void RegisterPlayer(PlayerCharacter player)
    {
        _consumables.RegisterItems(player.Inventory.GetInventorySlots(ItemSlot.Consumable));
        _keyItems.RegisterItems(player.Inventory.GetInventorySlots(ItemSlot.KeyItem));

        player.Inventory.OnInventoryChange += _consumables.UpdateContainer;
        player.Inventory.OnInventoryChange += _keyItems.UpdateContainer;
    }
}
