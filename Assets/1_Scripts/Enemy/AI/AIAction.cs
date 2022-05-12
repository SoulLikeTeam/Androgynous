using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAction : MonoBehaviour
{
    protected AIActionData _aiActionData;
    protected AIMovementData _aimovementData;
    protected EnemyAiBrain _enemyBrain;

    private void Awake()
    {
        _aiActionData = transform.GetComponentInParent<AIActionData>();
        _aimovementData = transform.GetComponentInParent<AIMovementData>();
        _enemyBrain = transform.GetComponentInParent<EnemyAiBrain>();

        ChidAwake();
    }
    protected virtual void ChidAwake()
    {
        //
    }
    public abstract void TakeAction();
}
