using KaynirGames.AI;
using System.Collections;
using UnityEngine;

public class EnemyWait : BaseState<EnemyStateKey>
{
    private EnemyAI _enemyAI = null;
    private float _waitingTime = 0; // Время бездействия.
    private bool _isWaiting = false; // Нахождение в состоянии бездействия.

    public EnemyWait(EnemyAI enemyAI, float waitingTime)
    {
        _enemyAI = enemyAI;
        _waitingTime = waitingTime;
    }

    public override void EnterState()
    {
        Debug.Log("Waiting for my next action.");
    }

    public override BaseState<EnemyStateKey> UpdateState()
    {
        if (!_enemyAI.IsMoving)
        {
            if (!_isWaiting)
            {
                _enemyAI.StartCoroutine(WaitForDelay(_waitingTime));
            }
        }
        return null;
    }

    public override void ExitState()
    {
        _isWaiting = false;
    }

    /// <summary>
    /// Корутина бездействия.
    /// </summary>
    private IEnumerator WaitForDelay(float delay)
    {
        _isWaiting = true;
        yield return new WaitForSeconds(delay);
        _enemyAI.SetTransition(EnemyStateKey.WaitComplete);
    }
}
