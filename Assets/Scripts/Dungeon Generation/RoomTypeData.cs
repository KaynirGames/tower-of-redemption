using UnityEngine;

/// <summary>
/// Набор типов комнат: стартовая (обязательная), пустая (для спавна монстров и лута), с магазином, с боссом (обязательная).
/// </summary>
public enum RoomType { Start, Empty, Shop, Boss }

[CreateAssetMenu(fileName = "NewRoomTypeData", menuName = "Scriptable Objects/Dungeon Generation/Room Type Data")]
public class RoomTypeData : ScriptableObject
{
    /// <summary>
    /// Наименование типа комнаты (используется для загрузки нужной сцены).
    /// </summary>
    public string Name;
    /// <summary>
    /// Тип комнаты.
    /// </summary>
    public RoomType RoomType;
}