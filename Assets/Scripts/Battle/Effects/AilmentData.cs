using UnityEngine;

[CreateAssetMenu(fileName = "AilmentName Data", menuName = "Scriptable Objects/Battle/Effects/Ailment Data")]
public class AilmentData : ScriptableObject
{
    [SerializeField] private TranslatedText _ailmentName = new TranslatedText("Ailment.AilmentID.Name");
    [SerializeField] private TranslatedText _displayFormat = new TranslatedText("Ailment.AilmentID.Format");
    [SerializeField] private Sprite _icon = null;

    public string AilmentName => _ailmentName.Value;
    public string DisplayFormat => _displayFormat.Value;
    public Sprite Icon => _icon;

    public bool TryRestartAilment(Character target, Effect ailmentEffect)
    {
        var ailments = target.Effects.AilmentEffects;

        if (ailments.ContainsKey(this))
        {
            EffectInstance currentInstance = ailments[this];

            if (currentInstance.Effect == ailmentEffect)
            {
                currentInstance.ResetDuration();
                return true;
            }
            else
            {
                currentInstance.RemoveEffect();
                return false;
            }
        }

        return false;
    }
}
