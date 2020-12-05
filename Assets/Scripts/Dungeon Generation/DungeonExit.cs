using UnityEngine;

public class DungeonExit : MonoBehaviour
{
    private GameMaster _gameMaster;

    private void Start()
    {
        _gameMaster = GameMaster.Instance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerCharacter player))
        {
            _gameMaster.SaveGame();
            _gameMaster.ContinueGame();
        }
    }
}
