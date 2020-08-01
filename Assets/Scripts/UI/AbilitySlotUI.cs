using System;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySlotUI : MonoBehaviour
{
    public static event Action<string, string> OnDescriptionCall = delegate { };

    [SerializeField] private Image _icon = null;

    private Ability _ability;

    public void AddAbility(Ability ability)
    {
        _ability = ability;
        _icon.sprite = ability.Icon;
        _icon.enabled = true;
    }

    public void RemoveAbility()
    {
        _ability = null;
        _icon.sprite = null;
        _icon.enabled = false;
    }

    public void CallDescription()
    {
        if (_ability != null)
        {
            OnDescriptionCall?.Invoke(_ability.Name, _ability.GetDisplayInfo());
        }
        else Debug.Log(name + " clicked");
    }
}
