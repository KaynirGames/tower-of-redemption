using UnityEngine;

/// <summary>
/// Умение персонажа.
/// </summary>
public abstract class Ability : ScriptableObject
{
    [Header("Основные параметры:")]
    [SerializeField] protected string _name = "New Ability";
    [SerializeField, TextArea(6,6)] protected string _description = "New Description";
    [SerializeField] protected Sprite _icon = null;
    [SerializeField] protected int _tier = 0;
    /// <summary>
    /// Название умения.
    /// </summary>
    public string Name => _name;
    /// <summary>
    /// Иконка умения.
    /// </summary>
    public Sprite Icon => _icon;
    /// <summary>
    /// Ранг умения.
    /// </summary>
    public int Tier => _tier;
    /// <summary>
    /// Получить информацию об умении для отображения.
    /// </summary>
    public abstract string GetDisplayInfo();
    /// <summary>
    /// Применить умение к цели.
    /// </summary>
    public abstract void Activate(CharacterStats target);
}