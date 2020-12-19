using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dungeon Stage Database", menuName = "Scriptable Objects/Game Databases/Dungeon Stage Database")]
public class DungeonStageDatabase : GameDatabase<DungeonStage>
{
    public List<DungeonStage> GetDungeonStageSet()
    {
        return new List<DungeonStage>(_objects);
    }
}
