using System;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public static event Action<string, string> OnDescriptionCall = delegate { };

    [SerializeField] private Image _icon = null;

    private Ability _skill;

    public void AddSkill(Ability skill)
    {
        _skill = skill;
        _icon.sprite = _skill.Icon;
        _icon.enabled = true;
    }

    public void RemoveSkill()
    {
        _skill = null;
        _icon.sprite = null;
        _icon.enabled = false;
    }

    public void CallDescription()
    {
        if (_skill != null)
        {
            OnDescriptionCall?.Invoke(_skill.Name, _skill.GetDescription());
        }
        else Debug.Log(name + " clicked");
    }
}
