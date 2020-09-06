using UnityEngine;

[CreateAssetMenu(fileName = "Energy +Amount", menuName = "Scriptable Objects/Battle/Recovery Types/Energy Recovery Flat")]
public class EnergyRecoveryFlat : RecoveryType
{
    public override void RecoverResource(CharacterStats target)
    {
        target.ChangeEnergy(_recoveryAmount);
    }

    public override string GetDescription()
    {
        return string.Format(GameTexts.Instance.EnergyRecoveryFlatFormat, _recoveryAmount);
    }
}
