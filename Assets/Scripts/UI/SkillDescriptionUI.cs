using UnityEngine;
using TMPro;
using System.Text;

public class SkillDescriptionUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText = null; // Текстовое поле для названия умения.
    [SerializeField] private TMP_Text _typeText = null; // Текстовое поле для типа умения.
    [SerializeField] private TMP_Text _descriptionText = null; // Текстовое поле для описания умения.

    private StringBuilder _stringBuilder = new StringBuilder(64, 64);

    public void ShowDescription(Skill skill)
    {
        _nameText.SetText(skill.SkillName);

        _stringBuilder.Length = 0;
        //_stringBuilder.Append(GameDictionary.SkillTypeNames[skill.GetType()]);
        _stringBuilder.Append("(Ранг: ");
        _stringBuilder.Append(skill.PowerTier.TierName);
        _stringBuilder.Append(")");

        _typeText.SetText(_stringBuilder);
        _descriptionText.text = skill.GetDescription();

        gameObject.SetActive(true);
    }

    public void HideDescription()
    {
        _nameText.text = string.Empty;
        _typeText.text = string.Empty;
        _descriptionText.text = string.Empty;

        gameObject.SetActive(false);
    }
}
