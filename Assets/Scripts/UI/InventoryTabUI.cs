using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class InventoryTabUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _titleField = null; // Текстовое поле для названия вкладки.
    [SerializeField] private UnityEvent _onActiveTab = null; // Событие при активации вкладки.
    /// <summary>
    /// Название вкладки инвентаря.
    /// </summary>
    public string Title => _titleField.text;
    /// <summary>
    /// Активировать вкладку инвентаря.
    /// </summary>
    public void Activate()
    {
        gameObject.SetActive(true);
        _onActiveTab?.Invoke();
    }
    /// <summary>
    /// Деактивировать вкладку инвентаря.
    /// </summary>
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
