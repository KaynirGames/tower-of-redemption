using UnityEngine;

/// <summary>
/// Ранг силы умения, определяющий модификатор при расчете урона, защиты и т.п.
/// </summary>
[CreateAssetMenu(fileName = "Tier_Name", menuName = "Scriptable Objects/Battle/Power Tier")]
public class PowerTier : ScriptableObject
{
    [SerializeField] private string _tierName = string.Empty;
    [SerializeField] private float _powerModifier = 1f;
    /// <summary>
    /// Название ранга силы умения.
    /// </summary>
    public string TierName => _tierName;
    /// <summary>
    /// Модификатор силы умения.
    /// </summary>
    public float PowerModifier => _powerModifier;
}