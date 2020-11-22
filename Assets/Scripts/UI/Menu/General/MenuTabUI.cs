using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class MenuTabUI : MonoBehaviour
{
    [Header("Общие параметры вкладки:")]
    [SerializeField] private Button _tabSelectionButton = null;
    [SerializeField] private LocalizedString _tabName = null;
    [SerializeField] private UnityEvent _onTabOpen = null;
    [SerializeField] private UnityEvent _onTabClose = null;

    private CanvasGroup _canvasGroup;

    public string TabName => _tabName.GetLocalizedString().Result;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void Open()
    {
        Toggle(true);
        _onTabOpen?.Invoke();
    }

    public virtual void Close()
    {
        _onTabClose.Invoke();
        Toggle(false);
    }

    public virtual void RegisterPlayer(PlayerCharacter player) { }

    protected virtual void Toggle(bool enable)
    {
        _tabSelectionButton.interactable = !enable;
        _canvasGroup.alpha = enable ? 1 : 0;
        _canvasGroup.blocksRaycasts = enable;
    }
}