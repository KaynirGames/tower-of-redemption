using UnityEngine;

[CreateAssetMenu(fileName = "AilmentName Data", menuName = "Scriptable Objects/Battle/Effects/Ailment Data")]
public class AilmentData : ScriptableObject
{
    [SerializeField] private TranslatedText _ailmentName = new TranslatedText("Ailment.AilmentID.Name");
    [SerializeField] private Color _ailmentTextColor = new Color(0, 0, 0, 255);
    [SerializeField] private Sprite _icon = null;

    public string AilmentName => _ailmentName.Value;
    public Sprite Icon => _icon;

    public string AilmentTextColorHtml { get; private set; }

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

    private void OnValidate()
    {
        AilmentTextColorHtml = ColorUtility.ToHtmlStringRGBA(_ailmentTextColor);
    }
}
