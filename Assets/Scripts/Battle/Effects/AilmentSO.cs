using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "Undefined Ailment SO", menuName = "Scriptable Objects/Battle/Ailment SO")]
public class AilmentSO : ScriptableObject
{
    [SerializeField] private Sprite _icon = null;
    [SerializeField] private LocalizedString _name = null;

    public Sprite Icon => _icon;
    public string Name => _name.GetLocalizedString().Result;

    public bool TryRestartAilment(Character target, EffectSO effect)
    {
        var ailments = target.Effects.AilmentEffects;

        if (ailments.ContainsKey(this))
        {
            Effect currentEffect = ailments[this];

            if (currentEffect.EffectSO == effect)
            {
                currentEffect.ResetDuration();
                return true;
            }
            else
            {
                currentEffect.RemoveEffect();
                return false;
            }
        }

        return false;
    }

    public void ShowAilmentNamePopup(Vector2 position)
    {
        PoolManager.Instance.Take(PoolTags.AilmentNamePopup.ToString())
                            .GetComponent<TextPopup>()
                            .Setup(Name, position);
    }
}
