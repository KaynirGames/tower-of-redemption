using UnityEngine;

/// <summary>
/// Ранг силы умения, определяющий модификатор при расчете урона, защиты и т.п.
/// </summary>
[CreateAssetMenu(fileName = "Tier Name", menuName = "Scriptable Objects/Battle/Power Tier")]
public class PowerTier : ScriptableObject
{
    [SerializeField] private TranslatedText _tierName = null;
    [SerializeField] private float _attackPower = 1f;
    [SerializeField] private float _statBuffPower = 0.1f;

    public string TierName => _tierName.Value;
    public float AttackPower => _attackPower;
    public float StatBuffPower => _statBuffPower;
}