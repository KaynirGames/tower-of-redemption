using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBookUI : MonoBehaviour
{
    [SerializeField] private Image _playerSpecIcon = null;
    [SerializeField] private GameObject _activeSkillsParent = null;
    [SerializeField] private GameObject _extraSkillsParent = null;
    [SerializeField] private GameObject _passiveSkillsParent = null;

    private AbilityBook _skillBook;
    private PlayerSpec _playerSpec;

    private void Awake()
    {
        Player.OnPlayerActive += SetSkillBook;
    }

    private void SetSkillBook(Player player)
    {
        _skillBook = player.SkillBook;
        _playerSpec = player.PlayerSpec;
    }
}
