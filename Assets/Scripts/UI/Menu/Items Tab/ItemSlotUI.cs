using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ItemSlotUI : MonoBehaviour, IDeselectHandler
{
    [SerializeField] private Image _icon = null;
    [SerializeField] private Button _useButton = null;
    [SerializeField] private TextMeshProUGUI _amountText = null;

    private Item _item;
    private MenuUI _menuUI;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        _menuUI = MenuUI.Instance;
    }

    public void UpdateSlot(Item item)
    {
        if (item == null)
        {
            ClearSlot();
        }
        else
        {
            FillSlot(item);
        }
    }

    public void ShowActionPopup()
    {
        _menuUI.ActionPopupUI.ShowActionPopup(_item,
                                              _rectTransform.position);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (!_menuUI.ActionPopupUI.IsSelectingAction)
        {
            _menuUI.ClearSelection();
        }
    }

    private void FillSlot(Item item)
    {
        _item = item;
        _icon.sprite = item.ItemSO.Icon;
        _icon.enabled = true;
        _amountText.enabled = item.ItemSO.CanUse;
        _amountText.SetText(_item.Amount.ToString());
        _useButton.interactable = true;
    }

    private void ClearSlot()
    {
        _icon.enabled = false;
        _icon.sprite = null;
        _amountText.SetText("");
        _useButton.interactable = false;
        _item = null;

        _menuUI.DescriptionUI.ClearDescriptionText();
    }
}
