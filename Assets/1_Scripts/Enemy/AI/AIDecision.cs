using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIDecision : MonoBehaviour
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
    public abstract bool MakeADecision();
    //이 함수를 실행하면 transition을 일으킬 것인지 아닌지 결정해서 bool로 반환
}
