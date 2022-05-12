using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : AgentAnimation
{
    public void EndMotion()
    {
        OnEndMotion?.Invoke();
    }
    public void ChackDamaged()
    {
        OnChackDamaged?.Invoke();
    }
}
