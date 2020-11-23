using UnityEngine;

public class ItemPickup : Interactable
{
    [SerializeField] private ItemSO _itemSO = null;

    public override void Interact()
    {
        PlayerCharacter.Active.Inventory.AddItem(_itemSO);
        _onInteraction?.Invoke();
        Destroy(gameObject);
    }
}