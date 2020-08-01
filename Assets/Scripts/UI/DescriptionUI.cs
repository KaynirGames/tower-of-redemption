using TMPro;
using UnityEngine;

public class DescriptionUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText = null;
    [SerializeField] private TMP_Text _descriptionText = null;

    private void Awake()
    {
        AbilitySlotUI.OnDescriptionCall += Display;
    }

    private void Display(string name, string description)
    {
        _nameText.text = name;
        _descriptionText.text = description;
    }
}
