using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Scriptable Objects/Battle/Abilities/Passive Ability")]
public class PassiveAbility : Ability
{
    public override void Activate(CharacterStats target)
    {
        
    }

    public override string GetDisplayInfo()
    {
        return "";
    }
}
