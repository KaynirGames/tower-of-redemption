using UnityEngine;

public class PlayerAttackHit : MonoBehaviour
{
    public static event BattleManager.OnBattleTrigger OnBattleTrigger = delegate { return false; };

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyCharacter enemy = other.GetComponent<EnemyCharacter>();

        if (enemy != null)
        {
            bool inBattle = OnBattleTrigger.Invoke(enemy, true);

            if (inBattle) { enemy.PrepareForBattle(); }
        }
    }
}
