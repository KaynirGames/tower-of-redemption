using UnityEngine;

public class BattleTester : MonoBehaviour
{
    public static event BattleManager.OnBattleTrigger OnBattleTrigger = delegate { return false; };

    [SerializeField] private EnemyCharacter _testEnemyPrefab = null;
    [SerializeField] private Transform _testSpawn = null;
    [SerializeField] Joystick _joystick = null;

    private PlayerCharacter _player;
    private EnemyCharacter _currentEnemy;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (_currentEnemy == null)
            {
                _currentEnemy = Instantiate(_testEnemyPrefab, _testSpawn.position, Quaternion.identity);
                if (_player == null) _player = PlayerManager.Instance.Player;
            }
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            _joystick.enabled = !_joystick.enabled;
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            _player.Stats.ChangeHealth(-20);
        }
    }
}
