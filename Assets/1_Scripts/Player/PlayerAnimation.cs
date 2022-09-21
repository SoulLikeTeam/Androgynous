using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimation : AgentAnimation
{
    [field:SerializeField]
    public UnityEvent OnInitMotion { get; set; }

    protected readonly int _heavyAttackHashStr = Animator.StringToHash("HeavyAttack");
    protected readonly int _parryHashStr = Animator.StringToHash("Parry");
    protected readonly int _waitHashStr = Animator.StringToHash("Wait");

    public override void AnimatePlayer(float velocity,bool value)
    {
        SetWalkAnimation(velocity > 0&& !value);
        SetRunAnimation(velocity>0&&value);
    }
    public void WaitParry(bool value)
    {
        _agentAnimator.SetBool(_waitHashStr, value);
    }
    public void ParryAniamtion()
    {
        _agentAnimator.SetTrigger(_parryHashStr);
    }
    public void HeavyAttack()
    {
        _agentAnimator.SetTrigger(_heavyAttackHashStr);
    }
    public void EndMotion()
    {
        OnEndMotion?.Invoke();
    }
    public void InitMotion()
    {
        OnInitMotion?.Invoke();
    }
    public void ChackDamaged()
    {
        OnChackDamaged?.Invoke();
    }
}
