using UnityEngine;

[CreateAssetMenu(fileName = "Player Database", menuName = "Scriptable Objects/Game Databases/Player Database")]
public class PlayerDatabase : GameDatabase<PlayerCharacter>
{
    public GameObject CreatePlayer(string playerSpecID)
    {
        PlayerCharacter prefab = Find(player => player.PlayerSpec.ID == playerSpecID);
        return Instantiate(prefab.gameObject);
    }
}
