using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUI : MonoBehaviour
{
    [Header("Отображаемые данные об игроке:")]
    [SerializeField] private ResourceDisplayUI _playerResourceDisplay = null;
    [Header("Отображаемые данные о враге:")]
    [SerializeField] private ResourceDisplayUI _enemyResourceDisplay = null;
    [SerializeField] private StatDisplayUI _enemyStatsDisplay = null;
    [SerializeField] private EfficacyDisplayUI _enemyEfficacyDisplay = null;
}
