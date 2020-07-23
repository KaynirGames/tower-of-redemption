using UnityEngine;

/// <summary>
/// Эффективность воздействия стихийного элемента на персонажа.
/// </summary>
[System.Serializable]
public class ElementEfficacy
{
    [SerializeField] private Stat _efficacyRate = null;
    [SerializeField] private MagicElement _element = MagicElement.Fire;
    /// <summary>
    /// Коэффициент эффективности (0 - нет эффекта, 100 - нормальный урон, 200 - урон х2 и т.п.).
    /// </summary>
    public Stat EfficacyRate => _efficacyRate;
    /// <summary>
    /// Стихийный элемент.
    /// </summary>
    public MagicElement Element => _element;

    public ElementEfficacy(float rateValue, MagicElement element)
    {
        _efficacyRate = new Stat(rateValue);
        _element = element;
    }
}
