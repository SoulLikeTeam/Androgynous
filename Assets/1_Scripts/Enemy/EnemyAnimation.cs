using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : AgentAnimation
{
    private EnemyAiBrain _enemyAiBrain;
    protected readonly int _liveHashStr = Animator.StringToHash("Live");
    protected override void ChildAwake()
    {
        _enemyAiBrain = GetComponentInParent<EnemyAiBrain>();
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
