using TMPro;
using UnityEngine;

public class DescriptionUI : MonoBehaviour
{
    public delegate void OnDescriptionCall(string name, string type, string description);

    [SerializeField] private TextMeshProUGUI _nameField = null;
    [SerializeField] private TextMeshProUGUI _typeField = null;
    [SerializeField] private TextMeshProUGUI _descriptionField = null;

    public void FillDescriptionPanel(string name, string type, string description)
    {
        _nameField.SetText(name);
        _typeField.SetText(type);
        _descriptionField.SetText(description);
    }

    public void ClearDescriptionPanel()
    {
        _nameField.SetText("");
        _typeField.SetText("");
        _descriptionField.SetText("");
    }
}
