using UnityEngine;

[CreateAssetMenu(fileName = "NewActiveAbility", menuName = "Scriptable Objects/Battle/Abilities/Active Ability")]
public class ActiveAbility : Ability
{
    [Header("Параметры активного умения:")]
    [SerializeField] private float _energyCost = 0; // Затраты очков энергии.
    [SerializeField] private float _cooldown = 0; // Время перезарядки умения.
    /// <summary>
    /// Затраты очков энергии.
    /// </summary>
    public float EnergyCost => _energyCost;

    public override void Activate(CharacterStats source, CharacterStats target)
    {
        foreach (Effect effect in _effects)
        {
            if (effect.TargetType == TargetType.Self)
            {
                effect.ApplyEffect(source);
            }
            else if (effect.TargetType == TargetType.Enemy)
            {
                effect.ApplyEffect(target);
            }
        }
    }

    public override string GetDisplayInfo()
    {
        string displayInfo = string.Concat(
            _description, "\n",
            "Затраты энергии: ", _energyCost, "\n",
            "Время перезарядки: ", _cooldown, "\n",
            "Накладываемые эффекты:"
            );

        foreach (Effect effect in _effects)
        {
            displayInfo += "\n" + effect.GetDisplayInfo();
        }

        return displayInfo;
    }

    private void OnEnable()
    {
        if (_effects.Count > 0)
        {
            _effects.Sort(CompareEffectPriority);
        }
    }
}