using System.Text;
using TMPro;
using UnityEngine;

/// <summary>
/// Описание умения персонажа.
/// </summary>
public class SkillDescriptionUI : MonoBehaviour
{
    [SerializeField] private GameObject _skillDescriptionParent = null;
    [Header("Отображение данных об умении:")]
    [SerializeField] private TextMeshProUGUI _nameField = null; // Текстовое поле для названия умения.
    [SerializeField] private TextMeshProUGUI _typeField = null; // Текстовое поле для типа умения.
    [SerializeField] private TextMeshProUGUI _costField = null; // Текстовое поля для стоимости умения.
    [SerializeField] private TextMeshProUGUI _cooldownField = null; // Текстовое поле для перезарядки умения.
    [SerializeField] private TextMeshProUGUI _paramsField = null; // Текстовое поля для параметров умения.
    [SerializeField] private TextMeshProUGUI _descriptionField = null; // Текстовое поле для описания умения.

    private StringBuilder _stringBuilder = new StringBuilder(128, 128);

    public void ShowDescription(Skill skill)
    {
        _nameField.SetText(skill.SkillName);
        _typeField.SetText(skill.SkillType.TypeName);
        _costField.SetText(skill.Cost.ToString());
        _cooldownField.SetText(skill.Cooldown.ToString());

        _stringBuilder.Clear();
        skill.BuildParamsDescription(_stringBuilder);

        _paramsField.SetText(_stringBuilder);
        _descriptionField.SetText(skill.Description);

        _skillDescriptionParent.SetActive(true);
    }

    public void HideDescription()
    {
        _skillDescriptionParent.SetActive(false);
    }
}
