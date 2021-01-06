using UnityEngine;
using UnityEngine.Localization;

public class ItemPickup : Interactable
{
    [SerializeField] private ItemSO _itemSO = null;
    [SerializeField] private LocalizedString _pickupTextFormat = null;

    private Camera _camera;
    private SoundController _sounds;

    private void Awake()
    {
        _sounds = GetComponent<SoundController>();
    }

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

        _sounds.PlaySoundOneShot("PickupItem");

        _onInteraction?.Invoke();
        Destroy(gameObject);
    }

    private void CreatePickupItemPopup(string text, Vector2 position)
    {
        PoolManager.Instance.Take(PoolTags.PickupItemPopup.ToString())
                            .GetComponent<TextPopup>()
                            .Setup(text, position);
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