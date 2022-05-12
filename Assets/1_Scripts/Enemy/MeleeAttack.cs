using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : EnemyAttack
{
    private int _attackMode = 0;
    public override void OnAttack(int mode)
    {
        if(!_waitBeforeNextAttack)
        {
            _attackMode = mode;
            _agentAnimation.PlayAttackAnimation(mode+1);
            StartCoroutine(WaitBeforeAttackCoroutine(_attackData.afterCastDelay));
        }
    }

    public void CheckDamaged()
    {
        Vector2 minVec = _attackData.attackDatas[_attackMode].boxMin, maxVec = _attackData.attackDatas[_attackMode].boxMax;
        Vector2 vec = GetTarget().position - transform.position;
        if(vec.x<0)
        {
            maxVec.x = -maxVec.x;
        }
        Vector2 centerVec = (new Vector2(maxVec.x-minVec.x,maxVec.y-minVec.y) * 0.5f)+(Vector2)transform.position;
        if(Physics2D.OverlapBox(centerVec,new Vector2(Mathf.Abs(maxVec.x),maxVec.y),0,_layer))
        {
            Debug.Log("Hit!!");
            IHittable hittable = GetTarget().GetComponent<IHittable>();
            IKnockBack knockBack = GetTarget().GetComponent<IKnockBack>();
                
            hittable.GetHit(_attackData.attackDatas[_attackMode].damage,_attackData.attackDatas[_attackMode].criticalChance,gameObject);
            knockBack.KnockBack(vec,_attackData.attackDatas[_attackMode].knockBackValue,(float)_attackData.attackDatas[_attackMode].staggerValue * 0.01f);
            AttackFeedback?.Invoke();
        }
    }
    public void MotionEnd()
    {
        _agentAnimation.StopAttackAnimation();
    }
}
