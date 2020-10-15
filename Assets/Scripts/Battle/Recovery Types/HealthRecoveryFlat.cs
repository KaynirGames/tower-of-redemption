using UnityEngine;

[CreateAssetMenu(fileName = "Health +Amount", menuName = "Scriptable Objects/Battle/Recovery Types/Health Recovery Flat")]
public class HealthRecoveryFlat : RecoveryType
{
    public override void RecoverResource(CharacterStats target)
    {
        target.ChangeHealth(_recoveryAmount);
    }

    public override string GetDescription()
    {
        return "";
    }
}