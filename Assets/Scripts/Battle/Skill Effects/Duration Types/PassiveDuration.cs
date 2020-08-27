using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PassiveDuration", menuName = "Scriptable Objects/Battle/Duration Types/Passive Duration")]
public class PassiveDuration : DurationType
{
    public override void Execute(SkillEffect effect, CharacterStats target) { }

    public override void Terminate(SkillEffect effect, CharacterStats target) { }
}
