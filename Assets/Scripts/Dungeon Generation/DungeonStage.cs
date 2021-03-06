﻿using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "DungeonStage", menuName = "Scriptable Objects/Dungeon Generation/Dungeon Stage")]
public class DungeonStage : ScriptableObject, IIdentifiable
{
    [Header("Базовая информация об этаже:")]
    [SerializeField] private LocalizedString _stageName = null;
    [SerializeField] private SoundClipSO _stageTheme = null;
    [Header("Параметры создания этажа:")]
    [SerializeField] private int _minRouteLength = 1;
    [SerializeField] private int _maxRouteLength = 1;
    [SerializeField] private int _routeSpawnerCount = 1;
    [Header("Параметры комнат:")]
    [SerializeField] private Room _startRoomPrefab = null;
    [SerializeField] private Room _bossRoomPrefab = null;
    [SerializeField] private SpawnTable _optionalRoomsTable = null;
    [SerializeField] private Light2D _globalLightPrefab = null;

    [SerializeField] private string _id = null;

    public string StageName => _stageName.GetLocalizedString().Result;
    public SoundClipSO StageTheme => _stageTheme;

    public int RandomRouteLength => Random.Range(_minRouteLength, _maxRouteLength + 1);
    public int RouteSpawnerCount => _routeSpawnerCount;

    public GameObject StartRoomPrefab => _startRoomPrefab.gameObject;
    public GameObject BossRoomPrefab => _bossRoomPrefab.gameObject;
    public GameObject RandomOptionalRoomPrefab => _optionalRoomsTable.ChooseRandom() as GameObject;
    public GameObject GlobalLightPrefab => _globalLightPrefab.gameObject;

    public string ID => _id;

#if UNITY_EDITOR
    private void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);
        _id = AssetDatabase.AssetPathToGUID(path);
    }
#endif
}