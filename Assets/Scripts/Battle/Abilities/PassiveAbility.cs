using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPassiveAbility", menuName = "Scriptable Objects/Battle/Abilities/Passive Ability")]
public class PassiveAbility : Ability
{
    public override void Activate(CharacterStats source, CharacterStats target)
    {
        
    }

    public override string GetDisplayInfo()
    {
        return "";
    }
}
