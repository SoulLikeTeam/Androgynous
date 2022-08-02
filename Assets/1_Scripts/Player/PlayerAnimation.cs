using UnityEngine;

public class PlayerAnimation : AgentAnimation
{
    protected readonly int _heavyAttackHashStr = Animator.StringToHash("HeavyAttack");

    public override void AnimatePlayer(float velocity,bool value)
    {
        SetWalkAnimation(velocity > 0&& !value);
        SetRunAnimation(velocity>0&&value);
    }
    public void HeavyAttack()
    {
        _agentAnimator.SetTrigger(_heavyAttackHashStr);
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
