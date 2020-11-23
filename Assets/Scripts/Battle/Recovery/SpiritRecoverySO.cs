using UnityEngine;

[CreateAssetMenu(fileName = "Spirit Recovery SO #", menuName = "Scriptable Objects/Battle/Recovery/Spirit Recovery SO")]
public class SpiritRecoverySO : RecoverySO
{
    [SerializeField] private float _recoveryValue = 0f;
    [SerializeField] private bool _isPercent = false;

    public override void ApplyRecovery(Character target)
    {
        float spiritAmount = _isPercent
            ? GetSpiritPercent(target)
            : _recoveryValue;

        target.Stats.ChangeSpirit(spiritAmount);
    }

    private float GetSpiritPercent(Character target)
    {
        return target.Stats.Spirit.MaxValue.GetFinalValue()
               * _recoveryValue
               / 100f;
    }
}
