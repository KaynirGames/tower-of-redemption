using UnityEngine;

public class MenuTabUI : MonoBehaviour
{
    [SerializeField] private GameObject _tabPanel = null;
    [SerializeField] private GameObject _tabSelectionButton = null;
    [SerializeField] private GameObject _tabActiveImageBox = null;

    public virtual void Toggle(bool enable)
    {
        _tabPanel.SetActive(enable);
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
