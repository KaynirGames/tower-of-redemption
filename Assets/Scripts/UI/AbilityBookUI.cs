using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBookUI : MonoBehaviour
{
    [SerializeField] private Image _playerSpecIcon = null;
    [SerializeField] private GameObject _activeSlotsParent = null;
    [SerializeField] private GameObject _passiveSlotsParent = null;
    [SerializeField] private GameObject _specialSlotsParent = null;

    private AbilityBook _abilityBook;
    private PlayerSpec _playerSpec;

    private AbilitySlotUI[] _activeSlots;
    private AbilitySlotUI[] _passiveSlots;
    private AbilitySlotUI[] _specialSlots;

    private void Awake()
    {
        Player.OnPlayerActive += SetAbilityBook;
        _activeSlots = _activeSlotsParent.GetComponentsInChildren<AbilitySlotUI>();
        _passiveSlots = _passiveSlotsParent.GetComponentsInChildren<AbilitySlotUI>();
        _specialSlots = _specialSlotsParent.GetComponentsInChildren<AbilitySlotUI>();
    }

    private void SetAbilityBook(Player player)
    {
        _abilityBook = player.AbilityBook;
        _playerSpec = player.PlayerSpec;
    }
}
