using UnityEngine;

public abstract class RecoveryType : ScriptableObject
{
    [SerializeField] protected TranslatedText _descriptionFormat = new TranslatedText("Recovery.Name");

    public abstract void ApplyRecovery(Character target);

    public abstract string GetDescription();
}
