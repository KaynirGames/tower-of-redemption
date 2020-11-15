using UnityEngine;

[CreateAssetMenu(fileName = "Energy Recovery SO #", menuName = "Scriptable Objects/Battle/Recovery/Energy Recovery SO")]
public class EnergyRecoverySO : RecoverySO
{
    [SerializeField] private float _recoveryValue = 0f;
    [SerializeField] private bool _isPercent = false;

    public override void ApplyRecovery(Character target)
    {
        float energyAmount = _isPercent
            ? GetEnergyPercent(target)
            : _recoveryValue;

        target.Stats.ChangeEnergy(energyAmount);
    }

    private float GetEnergyPercent(Character target)
    {
        return target.Stats.Energy.MaxValue.GetFinalValue()
               * _recoveryValue
               / 100f;
    }
}
