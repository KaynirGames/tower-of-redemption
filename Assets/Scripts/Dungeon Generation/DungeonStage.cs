using UnityEngine;

[CreateAssetMenu(fileName = "NewDungeonStage", menuName = "Scriptable Objects/Dungeon Generation/Dungeon Stage")]
public class DungeonStage : ScriptableObject
{
    /// <summary>
    /// Название этажа подземелья.
    /// </summary>
    public string Name;
    /// <summary>
    /// Минимальная длина коридора (количество комнат) на этаже.
    /// </summary>
    public int MinRouteLength;
    /// <summary>
    /// Максимальная длина коридора (количество комнат) на этаже.
    /// </summary>
    public int MaxRouteLength;
    /// <summary>
    /// Количество создателей коридоров, распределяющих их на этаже.
    /// </summary>
    public int RouteSpawnerCount;
    /// <summary>
    /// Список возможных типов стартовой комнаты на этаже.
    /// </summary>
    public RoomTypeData[] StartRoomTypes;
    /// <summary>
    /// Список возможных типов комнаты с боссом на этаже.
    /// </summary>
    public RoomTypeData[] BossRoomTypes;
    /// <summary>
    /// Список возможных опциональных типов комнат на этаже.
    /// </summary>
    public RoomTypeData[] OptionalRoomTypes;
}
