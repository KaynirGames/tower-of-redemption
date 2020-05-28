using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDungeonStage", menuName = "Dungeon Generation/Dungeon Stage")]
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
}
