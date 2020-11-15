using UnityEngine;

[CreateAssetMenu(fileName = "Health Recovery SO #", menuName = "Scriptable Objects/Battle/Recovery/Health Recovery SO")]
public class HealthRecoverySO : RecoverySO
{
    [SerializeField] private float _recoveryValue = 0f;
    [SerializeField] private bool _isPercent = false;

    public override void ApplyRecovery(Character target)
    {
        float healthAmount = _isPercent
            ? GetHealthPercent(target)
            : _recoveryValue;

        target.Stats.ChangeEnergy(healthAmount);
    }

    private float GetHealthPercent(Character target)
    {
        return target.Stats.Health.MaxValue.GetFinalValue()
               * _recoveryValue
               / 100f;
    }
}