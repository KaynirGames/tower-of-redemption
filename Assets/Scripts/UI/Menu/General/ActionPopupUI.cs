using UnityEngine;
using UnityEngine.EventSystems;

public class ActionPopupUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public event DescriptionUI.OnDescriptionCall OnItemDescriptionCall = delegate { };

    [SerializeField] private GameObject _useButton = null;
    [SerializeField] private RectTransform _popupRect = null;

    public bool IsSelectingAction { get; private set; }

    private CanvasGroup _canvasGroup;
    private Item _currentItem;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void ShowActionPopup(Item item, Vector2 position)
    {
        _currentItem = item;

        _popupRect.position = position;
        _useButton.SetActive(item.ItemSO.CanUse);

        Toggle(true);
    }

    public void UseItem()
    {
        _currentItem.Use(PlayerCharacter.Active);
    }

    public void ShowItemDescription()
    {
        OnItemDescriptionCall.Invoke(_currentItem.ItemSO.Name,
                                     _currentItem.ItemSO.Type,
                                     _currentItem.ItemSO.Description);
    }

    public void Toggle(bool enable)
    {
        _canvasGroup.alpha = enable ? 1 : 0;
        _canvasGroup.blocksRaycasts = enable;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IsSelectingAction = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsSelectingAction = false;
    }
}
