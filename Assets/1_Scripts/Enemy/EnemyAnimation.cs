using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : AgentAnimation
{
    private EnemyAiBrain _enemyAiBrain;
    protected readonly int _liveHashStr = Animator.StringToHash("Live");
    protected readonly int _guardHashStr = Animator.StringToHash("Guard");
    protected override void ChildAwake()
    {
        _enemyAiBrain = GetComponentInParent<EnemyAiBrain>();
    }
    public void GuardAnimation()
    {
        _agentAnimator.SetTrigger(_guardHashStr);
    }
    public void LiveEnemy()
    {
        _agentAnimator.SetTrigger(_liveHashStr);
    }
    public void SetEndOfAttackAnimation()
    {
        _enemyAiBrain.SetAttackState(false);
    }
    public void EndMotion()
    {
        OnEndMotion?.Invoke();
    }
    public void ChackDamaged()
    {
        OnChackDamaged?.Invoke();
    }
}
