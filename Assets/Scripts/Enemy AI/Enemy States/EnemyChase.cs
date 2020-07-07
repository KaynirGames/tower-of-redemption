using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KaynirGames.AI;

public class EnemyChase : BaseState<EnemyStateKey>
{
    public override void EnterState()
    {
        Debug.Log("Begin chase.");
    }

    public override BaseState<EnemyStateKey> UpdateState()
    {
        Debug.Log("Stop chase.");
        return null;
    }

    public override void ExitState()
    {
        Debug.Log("Chasing...");
    }
}
