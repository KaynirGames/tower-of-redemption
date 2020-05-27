using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonStageManager : MonoBehaviour
{
    /// <summary>
    /// Список комнат, присутствующих на уровне подземелья.
    /// </summary>
    public List<Room> loadedRooms = new List<Room>();
}
