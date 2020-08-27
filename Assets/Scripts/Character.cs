using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public CharacterStats Stats { get; protected set; }
    public SkillBook SkillBook { get; protected set; }

    protected abstract void Die();
}
