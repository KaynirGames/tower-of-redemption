using KaynirGames.AI;
using System.Collections;
using UnityEngine;

public class EnemyWait : BaseState<EnemyStateKey>
{
    private EnemyAI _enemyAI = null;

    private WaitForSeconds _waitingTime;
    private Coroutine _lastWaitRoutine;

    public EnemyWait(EnemyAI enemyAI, float waitingTime)
    {
        _enemyAI = enemyAI;

        _waitingTime = new WaitForSeconds(waitingTime);
    }

    public override void EnterState()
    {
        _lastWaitRoutine = _enemyAI.StartCoroutine(WaitRoutine());
    }

    public override BaseState<EnemyStateKey> UpdateState()
    {
        return null;
    }

    public override void ExitState()
    {
        _enemyAI.StopCoroutine(_lastWaitRoutine);
    }

    private IEnumerator WaitRoutine()
    {
        yield return _waitingTime;
        _enemyAI.SetTransition(EnemyStateKey.WaitComplete);
    }
}
