using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAiBrain : MonoBehaviour, IAgnetInput
{
    [field:SerializeField]
    public UnityEvent<Vector2> OnMovementKeyPress {get;set;}
    [field:SerializeField]
    public UnityEvent<float> OnFaceDirection {get;set;}
    [field:SerializeField]
    public UnityEvent<bool,int> OnAttackeyPress {get;set;}
    [field:SerializeField]
    public UnityEvent<bool> OnJumpKeyPress {get;set;}
    [field:SerializeField]
    public UnityEvent<float> OnRunKeyPress {get;set;}

    [SerializeField]
    private AIState _currentState;

    public Transform target;

    private void Start() {
        target = GameManager.Instance.PlayerTrm;
    }

    public void Attack(int mode)
    {   
        OnAttackeyPress?.Invoke(true,mode);
    }

    public void Move(Vector2 moveDirection,Vector2 targetPos)
    {
        OnMovementKeyPress?.Invoke(new Vector2(moveDirection.x,0));
        OnFaceDirection?.Invoke(targetPos.x);
    }

    public void ChangeState(AIState state)
    {
        _currentState = state;
    }
    private void Update() {
        if(target == null)
        {
            OnMovementKeyPress?.Invoke(Vector2.zero);
        }
        else
        {
            _currentState.UpdateState();
        }
    }
}
