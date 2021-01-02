using UnityEngine;
using UnityEngine.Localization;

public class ItemPickup : Interactable
{
    [SerializeField] private ItemSO _itemSO = null;
    [SerializeField] private LocalizedString _pickupTextFormat = null;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    public override void Interact()
    {
        PlayerCharacter player = PlayerCharacter.Active;

        CreatePickupItemPopup(_pickupTextFormat.GetLocalizedString(_itemSO.Name).Result,
                              _camera.transform.position);

        player.Inventory.AddItem(_itemSO);

        _onInteraction?.Invoke();
        Destroy(gameObject);
    }

    private void CreatePickupItemPopup(string text, Vector2 position)
    {
        string tag = AssetManager.Instance.PickupItemPopup.tag;
        TextPopup textPopup = PoolManager.Instance.Take(tag)
                                                  .GetComponent<TextPopup>();
        textPopup.Setup(text, position);
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