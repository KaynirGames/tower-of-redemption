using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "EnergyRecovery#", menuName = "Scriptable Objects/Battle/Recovery Types/Energy Recovery")]
public class EnergyRecovery : RecoveryType
{
    [SerializeField] private float _recoveryValue = 0f;
    [SerializeField] private bool _isPercent = false;

    public override void ApplyRecovery(Character target)
    {
        target.Stats.ChangeEnergy(_recoveryValue);
    }

    public override string GetDescription()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append(Mathf.Abs(_recoveryValue));

        if (_isPercent) { builder.Append("%"); }

        return string.Format(_descriptionFormat.Value,
                             builder.ToString());
    }
}
