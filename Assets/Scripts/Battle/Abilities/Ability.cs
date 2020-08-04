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
    [SerializeField] protected Sprite _icon = null; // Иконка умения.
    [SerializeField] protected BookSlot _bookSlot; // Слот в книге умений.
    [SerializeField] protected float _cost = 0f; // Стоимость умения.
    [SerializeField] protected float _cooldown = 0f; // Время перезарядки умения.
    [SerializeField] protected List<Effect> _effects = new List<Effect>(); // Список эффектов умения.
    /// <summary>
    /// Название умения.
    /// </summary>
    public string Name => _name;
    /// <summary>
    /// Иконка умения.
    /// </summary>
    public Sprite Icon => _icon;
    /// <summary>
    /// Занимаемый слот в книге умений.
    /// </summary>
    public BookSlot BookSlot => _bookSlot;
    /// <summary>
    /// Получить описание умения.
    /// </summary>
    public abstract string GetDescription();
    /// <summary>
    /// Активировать умение.
    /// </summary>
    public abstract void Activate(CharacterStats user, CharacterStats target);
}