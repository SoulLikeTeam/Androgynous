using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : MonoBehaviour
{
    private EnemyAiBrain _enemyAiBrain = null;
    public List<AIAction> _actions = new List<AIAction>();
    public List<AITransition> _transitions = new List<AITransition>();
    
    private void Awake() {
        _enemyAiBrain  = GetComponentInParent<EnemyAiBrain>();
    }

    public void UpdateState()
    {
        //내 상태에서 수행할 액션을 전부 수행하고
        foreach(AIAction action in _actions)
        {
            action.TakeAction(); //이렇게 하면 어떤 액션이든 수행
        }
        //현재 상태에서 다른 상태로 전이가 이뤄져야하는지 체크하고
        //그에 따라 전이를 일으키거나 가만 있는다.

        foreach(AITransition transition in _transitions)
        {
            bool result = false;
            foreach(AIDecision decision in transition.decisions)
            {
                result = decision.MakeADecision();
                if(!result) break;
            }

            if(result) //모든 조건이 성공한다면
            {
                if(transition.positiveState != null)
                {
                    _enemyAiBrain.ChangeState(transition.positiveState);
                    return;
                }
            }
            else //조건 중 하나라고 실패하면
            {
                if(transition.negativeState != null)
                {
                    _enemyAiBrain.ChangeState(transition.negativeState);
                    return;
                }
            }
        }
    }
}
