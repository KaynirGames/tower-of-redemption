using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] private Image _icon = null;
    [SerializeField] private Button _useButton = null;
    [SerializeField] private TextMeshProUGUI _amountText = null;

    private Item _item;
    private ActionPopupUI _actionPopupUI;

    private void Start()
    {
        _actionPopupUI = AssetManager.Instance.ActionPopup;
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
        _actionPopupUI.ShowActionPopup(_item);
    }

    private void FillSlot(Item item)
    {
        _item = item;
        _icon.sprite = item.ItemSO.Icon;
        _amountText.SetText(_item.Amount.ToString());
        _useButton.interactable = true;
    }

    private void ClearSlot()
    {
        _icon.sprite = null;
        _amountText.SetText("");
        _useButton.interactable = false;
        _item = null;
    }
}
