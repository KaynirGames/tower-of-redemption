using UnityEngine;
using UnityEngine.Localization;

public class MenuTabUI : MonoBehaviour
{
    [SerializeField] private Canvas _tabCanvas = null;
    [SerializeField] private GameObject _tabSelectionButton = null;
    [SerializeField] private GameObject _tabActiveImageBox = null;
    [SerializeField] private LocalizedString _tabName = null;

    public string TabName => _tabName.GetLocalizedString().Result;

    public virtual void Toggle(bool enable)
    {
        _tabCanvas.enabled = enable;
    }

    public virtual void Open()
    {
        _tabSelectionButton.SetActive(false);
        _tabActiveImageBox.SetActive(true);
        Toggle(true);
    }

    public virtual void Close()
    {
        _tabSelectionButton.SetActive(true);
        _tabActiveImageBox.SetActive(false);
        Toggle(false);
    }
}
