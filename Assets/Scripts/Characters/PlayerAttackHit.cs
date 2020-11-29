using UnityEngine;

public class PlayerAttackHit : MonoBehaviour
{
    public static event BattleManager.OnBattleTrigger OnBattleTrigger = delegate { return false; };

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out EnemyCharacter enemy))
        {
            bool inBattle = OnBattleTrigger.Invoke(enemy, true);

            if (inBattle)
            {
                enemy.PrepareForBattle();
            }
        }
    }
}
