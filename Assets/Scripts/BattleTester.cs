using UnityEngine;

public class BattleTester : MonoBehaviour
{
    public static event BattleManager.OnBattleTrigger OnBattleTrigger = delegate { return false; };

    [SerializeField] private EnemyCharacter _testEnemyPrefab = null;
    [SerializeField] private Transform _testSpawn = null;

    private PlayerCharacter _player;

    private void Start()
    {
        Debug.Log(Room.LoadedRooms.Count + " room(s) are loaded.");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Instantiate(_testEnemyPrefab, _testSpawn.position, Quaternion.identity);
            if (_player == null) _player = PlayerManager.Instance.Player;
        }
    }
}
