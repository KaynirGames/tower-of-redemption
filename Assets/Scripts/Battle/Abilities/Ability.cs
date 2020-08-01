using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Умение персонажа.
/// </summary>
public abstract class Ability : ScriptableObject
{
    [Header("Базовые параметры умения:")]
    [SerializeField] protected string _name = "Ability"; // Название умения.
    [SerializeField, TextArea(5,5)] protected string _description = "Description"; // Описание умения.
    [SerializeField] protected AbilityType _abilityType = AbilityType.Active; // Тип умения.
    [SerializeField] protected Sprite _icon = null; // Иконка умения.
    [SerializeField] protected List<Effect> _effects = new List<Effect>(); // Список эффектов умения.
    /// <summary>
    /// Название умения.
    /// </summary>
    public string Name => _name;
    /// <summary>
    /// Тип умения.
    /// </summary>
    public AbilityType AbilityType => _abilityType;
    /// <summary>
    /// Иконка умения.
    /// </summary>
    public Sprite Icon => _icon;
    /// <summary>
    /// Получить информацию об умении для отображения.
    /// </summary>
    public abstract string GetDisplayInfo();
    /// <summary>
    /// Активировать умение.
    /// </summary>
    public abstract void Activate(CharacterStats source, CharacterStats target);
    /// <summary>
    /// Сравнение эффектов по приоритету наложения.
    /// </summary>
    protected int CompareEffectPriority(Effect a, Effect b)
    {
        if (a.Priority < b.Priority) return -1;
        else if (a.Priority > b.Priority) return 1;
        return 0; // Если приоритеты эффектов равны.
    }
}