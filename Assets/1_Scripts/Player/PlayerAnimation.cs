
public class PlayerAnimation : AgentAnimation
{
    public override void AnimatePlayer(float velocity,bool value)
    {
        SetWalkAnimation(velocity > 0&& !value);
        SetRunAnimation(velocity>0&&value);
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
