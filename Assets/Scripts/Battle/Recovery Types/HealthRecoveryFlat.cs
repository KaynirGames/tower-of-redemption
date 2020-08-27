using UnityEngine;

[CreateAssetMenu(fileName = "HealthRecovery_Amount", menuName = "Scriptable Objects/Battle/Recovery Types/Health Recovery Flat")]
public class HealthRecoveryFlat : RecoveryType
{
    public override void RecoverResource(CharacterStats target)
    {
        target.RecoverHealth(_recoveryAmount);
    }

    public override string GetDescription()
    {
        return string.Format(GameTexts.Instance.HealthRecoveryFlatFormat, _recoveryAmount);
    }
}