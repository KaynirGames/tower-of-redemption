using UnityEngine;

/// <summary>
/// Текстовые данные о статусном эффекте.
/// </summary>
[CreateAssetMenu(fileName = "AilmentName Data", menuName = "Scriptable Objects/Text Data/Ailment Data")]
public class AilmentData : ScriptableObject
{
    [SerializeField] private TranslatedText _ailmentName = new TranslatedText("Ailment.AilmentID.Name");
    [SerializeField] private TextColorData _textColorData = null;
    [SerializeField] private Sprite _icon = null;

    public Sprite Icon => _icon;

    public string GetAilmentName()
    {
        return _textColorData.ColorText(_ailmentName.Value);
    }

    public bool TryRestartAilment(Character target, Effect effect)
    {
        var ailments = target.Effects.AilmentEffects;

        if (ailments.ContainsKey(this))
        {
            EffectInstance currentInstance = ailments[this];

            if (currentInstance.Effect == effect)
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
