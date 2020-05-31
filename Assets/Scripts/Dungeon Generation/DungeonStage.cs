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
    /// Данные об обязательной стартовой комнате на этаже.
    /// </summary>
    public RoomTypeData[] StartRoomTypes;
    /// <summary>
    /// Данные об обязательной комнате с боссом на этаже.
    /// </summary>
    public RoomTypeData[] BossRoomTypes;
    /// <summary>
    /// Данные о необязательных типах комнат на этаже.
    /// </summary>
    public RoomTypeData[] OptionalRoomTypes;
}
