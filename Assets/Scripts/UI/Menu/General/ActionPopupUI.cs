using UnityEngine;
using KaynirGames.Tools;

public class ActionPopupUI : MonoBehaviour
{
    public static event DescriptionUI.OnDescriptionCall OnItemDescriptionCall = delegate { };

    [SerializeField] private GameObject _useButton = null;

    private Item _currentItem;

    public void ShowActionPopup(Item item)
    {
        _currentItem = item;

        transform.position = KaynirTools.GetPointerRawPosition();
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
        gameObject.SetActive(enable);
    }
}
