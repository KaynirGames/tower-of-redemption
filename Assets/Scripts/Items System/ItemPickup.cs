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

    protected override void OnTriggerEnter2D(Collider2D other) { }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.TryGetComponent(out PlayerCharacter player))
        {
            Interact();
        }
    }
}