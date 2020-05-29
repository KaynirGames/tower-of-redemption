using UnityEngine;

[CreateAssetMenu(fileName = "NewDungeonStage", menuName = "Scriptable Objects/Dungeon Generation/Dungeon Stage")]
public class DungeonStage : ScriptableObject
{
    /// <summary>
    /// Название этажа подземелья
    /// </summary>
    public string Name;
    /// <summary>
    /// Минимальное количество комнат на этаже
    /// </summary>
    public int MinRoomAmount;
    /// <summary>
    /// Максимальное количество комнат на этаже
    /// </summary>
    public int MaxRoomAmount;
    /// <summary>
    /// Обязательная стартовая комната.
    /// </summary>
    public RoomType StartRoomType;
    /// <summary>
    /// Обязательная комната с боссом.
    /// </summary>
    public RoomType BossRoomtype;
    /// <summary>
    /// Типы возможных комнат на этаже.
    /// </summary>
    public RoomType[] FillerRoomTypes;
}
