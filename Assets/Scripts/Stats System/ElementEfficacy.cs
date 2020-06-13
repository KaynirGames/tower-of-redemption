using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Стихийные элементы (огонь, воздух, земля, вода, отсутствует).
/// </summary>
public enum MagicElement { Fire, Air, Earth, Water, None }
/// <summary>
/// Эффективность воздействия стихийного элемента на персонажа.
/// </summary>
[System.Serializable]
public class ElementEfficacy
{
    /// <summary>
    /// Коэффициент эффективности (0 - нет эффекта, 100 - нормальный урон, 200 - урон х2 и т.п.).
    /// </summary>
    public Stat EfficacyRate;
    /// <summary>
    /// Стихийный элемент.
    /// </summary>
    public MagicElement Element;

    public ElementEfficacy(float rateValue, MagicElement element)
    {
        EfficacyRate = new Stat(rateValue);
        Element = element;
    }
}
