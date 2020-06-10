﻿using UnityEngine;

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
    /// Данные об обязательной стартовой комнате на этаже.
    /// </summary>
    public RoomType StartRoomType;
    /// <summary>
    /// Данные об обязательной комнате с боссом на этаже.
    /// </summary>
    public RoomType BossRoomType;
    /// <summary>
    /// Таблица с вероятностями появления опциональных комнат.
    /// </summary>
    public SpawnTable OptionalRoomTypes;
}