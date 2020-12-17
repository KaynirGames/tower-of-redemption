using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Localization;


[CreateAssetMenu(fileName = "DungeonStage", menuName = "Scriptable Objects/Dungeon Generation/Dungeon Stage")]
public class DungeonStage : ScriptableObject, IIdentifiable
{
    [Header("Текстовая информация об этаже:")]
    [SerializeField] private LocalizedString _stageNameFormat = null;
    [Header("Параметры создания этажа:")]
    [SerializeField] private int _minFloorAmount = 1;
    [SerializeField] private int _maxFloorAmount = 1;
    [SerializeField] private int _minRouteLength = 1;
    [SerializeField] private int _maxRouteLength = 1;
    [SerializeField] private int _routeSpawnerCount = 1;
    [Header("Параметры комнат:")]
    [SerializeField] private Room _startRoomPrefab = null;
    [SerializeField] private Room _bossRoomPrefab = null;
    [SerializeField] private SpawnTable _optionalRoomsTable = null;
    [SerializeField] private Light2D _globalLightPrefab = null;

    public int RandomFloorAmount => Random.Range(_minFloorAmount, _maxFloorAmount + 1);
    public int RandomRouteLength => Random.Range(_minRouteLength, _maxRouteLength + 1);
    public int RouteSpawnerCount => _routeSpawnerCount;

    public GameObject StartRoomPrefab => _startRoomPrefab.gameObject;
    public GameObject BossRoomPrefab => _bossRoomPrefab.gameObject;
    public GameObject RandomOptionalRoomPrefab => _optionalRoomsTable.ChooseRandom() as GameObject;
    public GameObject GlobalLightPrefab => _globalLightPrefab.gameObject;

    public string ID { get; private set; }

    public string GetStageName(int floorNumber)
    {
        return _stageNameFormat.GetLocalizedString(floorNumber).Result;
    }

    private void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);
        ID = AssetDatabase.AssetPathToGUID(path);
    }
}