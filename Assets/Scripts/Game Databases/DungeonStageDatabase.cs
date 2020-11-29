using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dungeon Stage Database", menuName = "Scriptable Objects/Game Databases/Dungeon Stage Database")]
public class DungeonStageDatabase : GameDatabase<DungeonStage>
{
    public List<DungeonStage> GetDungeonStageSet()
    {
        List<DungeonStage> stages = new List<DungeonStage>();

        for (int i = 0; i < _objects.Count; i++)
        {
            int floorAmount = _objects[i].RandomFloorAmount;

            for (int j = 0; j < floorAmount; j++)
            {
                stages.Add(_objects[i]);
            }
        }

        return stages;
    }
}
