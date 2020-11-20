﻿using UnityEngine;

[CreateAssetMenu(fileName = "Undefined Ailment SO", menuName = "Scriptable Objects/Battle/Ailment SO")]
public class AilmentSO : ScriptableObject
{
    [SerializeField] private Sprite _icon = null;

    public Sprite Icon => _icon;

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
}