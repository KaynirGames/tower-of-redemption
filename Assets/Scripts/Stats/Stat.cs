using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    /// <summary>
    /// Базовое значение стата.
    /// </summary>
    public float BaseValue = 0;
    /// <summary>
    /// Список модификаторов базового значения стата.
    /// </summary>
    private readonly List<StatModifier> modifiers;
    /// <summary>
    /// Текущее значение стата с модификаторами.
    /// </summary>
    private float currentValue;
    /// <summary>
    /// Модификаторы стата изменились?
    /// </summary>
    private bool modifiersChanged;

    public Stat(float baseValue)
    {
        BaseValue = baseValue;
        modifiers = new List<StatModifier>();
        currentValue = baseValue;
        modifiersChanged = false;
    }
    /// <summary>
    /// Возвращает значение стата с учетом модификаторов (если они имеются).
    /// </summary>
    /// <returns></returns>
    public float Value
    {
        get
        {
            if (modifiersChanged)
            {
                currentValue = BaseValue;

                if (modifiers.Count > 0)
                {
                    modifiers.ForEach(mod => currentValue = mod.ApplyModifier(currentValue));
                }

                modifiersChanged = false;

                return Mathf.Round(currentValue);
            }
            else
            {
                return currentValue;
            }
        }
    }
    /// <summary>
    /// Добавить модификатор стата.
    /// </summary>
    public void AddModifier(StatModifier modifier)
    {
        if (modifier.Value != 0)
        {
            modifiers.Add(modifier);
            modifiersChanged = true;
        }
    }
    /// <summary>
    /// Убрать модификатор стата.
    /// </summary>
    public void RemoveModifier(StatModifier modifier)
    {
        if (modifier.Value != 0)
        {
            modifiers.Remove(modifier);
            modifiersChanged = true;
        }
    }
}
