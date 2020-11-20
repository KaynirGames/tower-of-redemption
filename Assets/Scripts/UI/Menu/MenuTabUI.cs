using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

public class MenuTabUI : MonoBehaviour
{
    [SerializeField] private Canvas _tabCanvas = null;
    [SerializeField] private Button _tabSelectionButton = null;
    [SerializeField] private LocalizedString _tabName = null;

    public string TabName => _tabName.GetLocalizedString().Result;

    public virtual void Toggle(bool enable)
    {
        _tabSelectionButton.interactable = !enable;
        _tabCanvas.enabled = enable;
    }

    protected virtual void RegisterPlayer(PlayerCharacter player) { }
}