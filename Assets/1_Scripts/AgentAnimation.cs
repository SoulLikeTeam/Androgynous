using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class AgentAnimation : MonoBehaviour
{
    protected Animator _agentAnimator;
    [field:SerializeField]
    public UnityEvent OnEndMotion {get;set;}
    [field:SerializeField]
    public UnityEvent OnChackDamaged {get;set;}
    //Hash
    protected readonly int _walkHashStr = Animator.StringToHash("Walk");
    protected readonly int _runHashStr = Animator.StringToHash("Run");
    protected readonly int _deathHashStr = Animator.StringToHash("Death");
    protected readonly int _attackHashStr = Animator.StringToHash("Attack");
    protected readonly int _attackModeHashStr = Animator.StringToHash("AttackMode");

    protected bool _isNotChangeFace = false;
    public bool IsNotChangeFace {get{return _isNotChangeFace;} set{_isNotChangeFace = value;}}

    private void Awake()
    {
        _agentAnimator = GetComponent<Animator>();
        ChildAwake();
    }

    protected virtual void ChildAwake()
    {
        // do nothing
    }

    public void SetWalkAnimation(bool value)
    {
        _agentAnimator.SetBool(_walkHashStr, value);
    }
    public void SetRunAnimation(bool value)
    {
        _agentAnimator.SetBool(_runHashStr, value);
    }

    public virtual void AnimatePlayer(float velocity,bool value)
    {
        SetWalkAnimation(velocity > 0);
    }
    
    public void PlayDeathAnimation()
    {
        _agentAnimator.SetTrigger(_deathHashStr);
    }
    public virtual void PlayAttackAnimation(int mode)
    {
        _agentAnimator.SetTrigger(_attackHashStr);
        _agentAnimator.SetInteger(_attackModeHashStr,mode);
    }
    public virtual void StopAttackAnimation()
    {
        _agentAnimator.SetInteger(_attackModeHashStr,0);
    }
    public void FaceDirection(float pointerDir)
    {
        if(_isNotChangeFace)return;
        if(pointerDir > 0)
        {
            _agentAnimator.transform.localScale = new Vector3(1f, 1f, 1f);
        }else if(pointerDir < 0)
        {
            _agentAnimator.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}
