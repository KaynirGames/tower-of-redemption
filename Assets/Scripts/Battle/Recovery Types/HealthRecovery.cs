using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthRecovery#", menuName = "Scriptable Objects/Battle/Recovery Types/Health Recovery")]
public class HealthRecovery : RecoveryType
{
    [SerializeField] private float _recoveryValue = 0f;
    [SerializeField] private bool _isPercent = false;

    public override void ApplyRecovery(Character target)
    {
        target.Stats.ChangeHealth(_recoveryValue);
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