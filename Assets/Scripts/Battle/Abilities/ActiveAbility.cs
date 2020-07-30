using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "NewActiveAbility", menuName = "Scriptable Objects/Battle/Abilities/Active Ability")]
public class ActiveAbility : Ability
{
    [Header("Параметры активного умения:")]
    [SerializeField] private float _energyCost = 0; // Затраты очков энергии.
    [SerializeField] private float _cooldown = 0; // Время перезарядки умения.
    [SerializeField] private List<Effect> _effects = new List<Effect>(); // Список эффектов умения.

    /// <summary>
    /// Затраты очков энергии.
    /// </summary>
    public float EnergyCost => _energyCost;

    public override void Activate(CharacterStats target)
    {
        foreach (Effect effect in _effects)
        {
            effect.Apply(target);
        }
    }

    public override string GetDisplayInfo()
    {
        string displayInfo = string.Concat(
            _description, "\n",
            "Затраты энергии: ", _energyCost, " ед.", "\n",
            "Время перезарядки: ", _cooldown, " сек."
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
            _effects = _effects.OrderBy(effect => effect.Priority).ToList();
        }
    }
}