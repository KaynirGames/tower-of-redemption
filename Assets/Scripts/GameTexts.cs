using UnityEngine;

/// <summary>
/// Переведенные игровые тексты, которые не удалось поместить на удобный объект.
/// </summary>
public class GameTexts : MonoBehaviour
{
    public static GameTexts Instance { get; private set; }
    [Header("Заголовки в описании:")]
    [SerializeField] private TranslatedText _damageTypeLabel = null; // Тип урона.
    [Header("Тексты в описании:")]
    [SerializeField] private TranslatedText _targetSelfText = null; // На себя.
    [SerializeField] private TranslatedText _targetEnemyText = null; // Враг.
    [Header("Единицы измерения:")]
    [SerializeField] private TranslatedText _secondsText = null; // сек.
    [SerializeField] private TranslatedText _spiritEnergyText = null; // ДЭ.
    [Header("Текстовые форматы:")]
    [SerializeField] private TranslatedText _healthRecoveryFlat = null;
    [SerializeField] private TranslatedText _energyRecoveryFlat = null;

    public string DamageTypeLabel => _damageTypeLabel.Value;

    public string TargetSelfLabel => _targetSelfText.Value;
    public string TargetEnemyLabel => _targetEnemyText.Value;

    public string SecondsText => _secondsText.Value;
    public string EnergyText => _spiritEnergyText.Value;

    public string HealthRecoveryFlatFormat => _healthRecoveryFlat.Value;
    public string EnergyRecoveryFlatFormat => _energyRecoveryFlat.Value;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
    }
}
