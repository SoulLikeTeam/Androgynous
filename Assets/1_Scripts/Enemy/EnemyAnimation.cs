using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : AgentAnimation
{
    protected readonly int _liveHashStr = Animator.StringToHash("Live");
    public void LiveEnemy()
    {
        _agentAnimator.SetTrigger(_liveHashStr);
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
