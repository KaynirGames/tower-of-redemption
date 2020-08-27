using UnityEngine;

public abstract class RecoveryType : ScriptableObject
{
    [SerializeField] protected float _recoveryAmount = 0f;

    public abstract void RecoverResource(CharacterStats target);

    public abstract string GetDescription();
}
