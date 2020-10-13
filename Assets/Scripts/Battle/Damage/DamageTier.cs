using UnityEngine;

/// <summary>
/// Величина, определяющая модификатор при расчете урона.
/// </summary>
[CreateAssetMenu(fileName = "Tier TierName", menuName = "Scriptable Objects/Battle/Damage/Damage Tier")]
public class DamageTier : ScriptableObject
{
    [SerializeField] private TranslatedText _tierName = null;
    [SerializeField] private float _attackPower = 1f;

    public string TierName => _tierName.Value;
    public float AttackPower => _attackPower;
}