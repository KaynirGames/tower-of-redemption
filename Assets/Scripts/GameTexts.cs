using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTexts : MonoBehaviour
{
    public static GameTexts Instance { get; private set; }
    [Header("Текстовые заголовки:")]
    [SerializeField] private TranslatedText _damageTypeLabel = null;
    [SerializeField] private TranslatedText _userEffectsLabel = null;
    [SerializeField] private TranslatedText _enemyEffectsLabel = null;
    [Header("Единицы измерения:")]
    [SerializeField] private TranslatedText _secondsText = null;
    /// <summary>
    /// Текстовый заголовок для типа урона.
    /// </summary>
    public string DamageTypeLabel => _damageTypeLabel.Value;
    /// <summary>
    /// Текстовый заголовок для эффектов на заклинателя.
    /// </summary>
    public string UserEffectsLabel => _userEffectsLabel.Value;
    /// <summary>
    /// Текстовый заголовок для эффектов на врага.
    /// </summary>
    public string EnemyEffectsLabel => _enemyEffectsLabel.Value;
    /// <summary>
    /// Текстовое отображение секунд.
    /// </summary>
    public string SecondsText => _secondsText.Value;
    


    private void Awake()
    {
        if (Instance == null) { Instance = this; }
    }
}
