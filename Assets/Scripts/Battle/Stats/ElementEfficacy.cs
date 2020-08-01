using UnityEngine;

/// <summary>
/// Эффективность воздействия стихийного элемента на персонажа.
/// </summary>
[System.Serializable]
public class ElementEfficacy
{
    [SerializeField] private float _efficacyRate = 100;
    [SerializeField] private ElementType _element = ElementType.Fire;
    /// <summary>
    /// Коэффициент эффективности (0 - нет эффекта, 100 - нормальный урон, 200 - урон х2 и т.п.).
    /// </summary>
    public float EfficacyRate => _efficacyRate;
    /// <summary>
    /// Стихийный элемент.
    /// </summary>
    public ElementType Element => _element;

    public ElementEfficacy(float efficacyRate, ElementType element)
    {
        _efficacyRate = efficacyRate;
        _element = element;
    }
}
