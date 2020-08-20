using TMPro;
using UnityEngine;

public class SkillDescriptionUI : MonoBehaviour
{
    [Header("Взаимоотключаемые объекты:")]
    [SerializeField] private GameObject _skillDescriptionPanel = null;
    [SerializeField] private GameObject _specDescriptionPanel = null;
    [Header("Отображение данных об умении:")]
    [SerializeField] private TextMeshProUGUI _nameField = null; // Текстовое поле для названия умения.
    [SerializeField] private TextMeshProUGUI _typeField = null; // Текстовое поле для типа умения.
    [SerializeField] private TextMeshProUGUI _tierField = null; // Текстовое поле для ранга умения.
    [SerializeField] private TextMeshProUGUI _costField = null; // Текстовое поля для стоимости умения.
    [SerializeField] private TextMeshProUGUI _cooldownField = null; // Текстовое поле для перезарядки умения.
    [SerializeField] private TextMeshProUGUI _paramsField = null; // Текстовое поля для параметров умения.
    [SerializeField] private TextMeshProUGUI _descriptionField = null; // Текстовое поле для описания умения.

    private void Awake()
    {
        SkillSlotUI.OnDescriptionCall += ShowDescription;
    }
    /// <summary>
    /// Показать описание объекта.
    /// </summary>
    public void ShowDescription(Skill skill)
    {
        _nameField.SetText(skill.SkillName);
        _typeField.SetText(skill.SkillType.TypeName);
        _tierField.SetText(skill.PowerTier.TierName);
        _costField.SetText(skill.Cost.ToString());
        _cooldownField.SetText(skill.Cooldown.ToString());
        _paramsField.SetText(skill.GetParamsDescription());
        _descriptionField.SetText(skill.Description);

        _specDescriptionPanel.SetActive(false);
        _skillDescriptionPanel.SetActive(true);
    }
}
