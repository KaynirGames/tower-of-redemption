using System.Collections.Generic;
using UnityEngine;

public class CharacterEffects : MonoBehaviour
{
    private Character _character;

    private List<StatBonus> _statsBonuses = new List<StatBonus>();
    private List<StatBuff> _buffEffects = new List<StatBuff>();

    private void Awake()
    {
        _character = GetComponent<Character>();
    }

    private void Update()
    {
        if (_buffEffects.Count > 0)
        {
            for (int i = _buffEffects.Count - 1; i >= 0; i--)
            {
                _buffEffects[i].Tick(Time.unscaledDeltaTime);
            }
        }
    }

    public void AddStatBonus(StatBonus bonus)
    {
        _statsBonuses.Add(bonus);
    }

    public void RemoveStatBonus(StatBonus bonus)
    {
        _statsBonuses.Remove(bonus);
    }

    public void AddBuffEffect(StatBuff buff)
    {
        StatBuff similarBuff = _buffEffects.Find(bf => bf.StatBuffType.IsIncompatible(buff.StatBuffType));

        if (similarBuff != null)
        {
            similarBuff.Remove(_character);
        }

        _buffEffects.Add(buff);
        // update UI
    }

    public void RemoveBuffEffect(StatBuff buff)
    {
        _buffEffects.Remove(buff);
        // update UI
    }
}
