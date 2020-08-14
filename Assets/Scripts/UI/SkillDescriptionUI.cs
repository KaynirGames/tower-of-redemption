using UnityEngine;
using TMPro;
using System.Text;

public class SkillDescriptionUI : MonoBehaviour
{
    [Header("Переведенные заголовки:")]
    [SerializeField] private TranslatedText _tierLabelKey = null;
    [SerializeField] private TranslatedText _costLabelKey = null;
    [SerializeField] private TranslatedText _cooldownLabelKey = null;
    [Header("Текстовые поля для отображения:")]
    [SerializeField] private TextMeshProUGUI _nameTextField = null; // Текстовое поле для названия умения.
    [SerializeField] private TextMeshProUGUI _typeTextField = null; // Текстовое поле для типа умения.
    [SerializeField] private TextMeshProUGUI _descriptionText = null; // Текстовое поле для описания умения.

    private StringBuilder _stringBuilder = new StringBuilder(64, 64);

    public void ShowDescription(Skill skill)
    {
        _nameTextField.SetText(skill.SkillName);

        _typeTextField.SetText(BuildTypeText(skill));

        _descriptionText.SetText(skill.GetDescription());

        gameObject.SetActive(true);
    }

    private StringBuilder BuildTypeText(Skill skill)
    {
        _stringBuilder.Clear();
        _stringBuilder.Append(skill.SkillType.TypeName);
        _stringBuilder.Append(" (");
        _stringBuilder.Append(_tierLabelKey.Value);
        _stringBuilder.Append(": ");
        _stringBuilder.Append(skill.PowerTier.TierName);
        _stringBuilder.Append(")");

        return _stringBuilder;
    }

    public void HideDescription()
    {
        _nameTextField.ClearMesh();
        _typeTextField.ClearMesh();
        _descriptionText.ClearMesh();

        gameObject.SetActive(false);
    }
}
