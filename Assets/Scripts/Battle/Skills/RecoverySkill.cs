using System.Text;
using UnityEngine;

/// <summary>
/// Восстанавливающее умение.
/// </summary>
[CreateAssetMenu(fileName = "NewRecoverySkill", menuName = "Scriptable Objects/Battle/Skills/Recovery Skill")]
public class RecoverySkill : Skill
{
    [Header("Параметры восстанавливающего умения:")]
    [SerializeField] private RecoveryType[] _recoveryTypes = null;

    public override void Activate(Character owner, Character opponent)
    {
        for (int i = 0; i < _recoveryTypes.Length; i++)
        {
            _recoveryTypes[i].RecoverResource(owner.Stats);
        }

    }

    public override void Deactivate(Character owner, Character opponent) { }

    public override void BuildParamsDescription(StringBuilder stringBuilder)
    {

    }
}
