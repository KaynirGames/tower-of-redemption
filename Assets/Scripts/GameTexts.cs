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
    /// <summary>
    /// Заголовок типа урона.
    /// </summary>
    public string DamageTypeLabel => _damageTypeLabel.Value;
    /// <summary>
    /// Текст типа цели: на себя.
    /// </summary>
    public string TargetSelfLabel => _targetSelfText.Value;
    /// <summary>
    /// Текст типа цели: враг.
    /// </summary>
    public string TargetEnemyLabel => _targetEnemyText.Value;
    /// <summary>
    /// Текстовое отображение секунд.
    /// </summary>
    public string SecondsText => _secondsText.Value;
    /// <summary>
    /// Текстовое отображение духовной энергии.
    /// </summary>
    public string EnergyText => _spiritEnergyText.Value;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
    }
}
