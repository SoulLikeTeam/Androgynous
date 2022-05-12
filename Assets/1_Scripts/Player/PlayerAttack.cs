using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : Attack
{
    private AgentMovement _agentMovement = null;

    protected bool _isInput = true;
    private LayerMask _layer;

    protected override void AwakeChild()
    {
        _agentMovement = GetComponent<AgentMovement>();
        _layer = LayerMask.GetMask("Enemy"); 
    }
    private void Update() {
        OnAttack(0);
    }
    public void IsInputAttackKey(bool value,int mode)
    {
        _isInput = value;
    }

    public override void OnAttack(int mode)
    {
        if(!_agentMovement.IsJump) return;
        if(_isInput&&!_isAttack&&Time.time >= _lastAttackTime + _attackData.afterCastDelay)
        {
            SetAttack();
            return;
        }

        if(_isAttack && _canAttack)
        {
            _attackTimer -= Time.deltaTime;
            if(_attackTimer <=0)
            {
                _attackTimer = 0;
                _assailCount = 0;
                _lastAttackTime = Time.time;

                _isAttack =false;
                _agentAnimation.StopAttackAnimation();
                PlayMovement();
            }
            else if(_isInput&&_canAttack)
            {
                SetAttack();
            }
        }

        if(_isAttack)
        {
            StopMovement();
        }
    }
    public void CheckDamaged()
    {

        
        IHittable hittable;
        IKnockBack knockBack;

        Vector2 minVec = _attackData.attackDatas[_assailCount-1].boxMin, maxVec = _attackData.attackDatas[_assailCount-1].boxMax;
        if(_agentMovement.MovementDirection.x<0)
        {
            maxVec.x = -maxVec.x;
        }
        
        Vector2 centerVec = (new Vector2(maxVec.x-minVec.x,maxVec.y-minVec.y) * 0.5f)+(Vector2)transform.position;
        Collider2D col = Physics2D.OverlapBox(centerVec,new Vector2(Mathf.Abs(maxVec.x),maxVec.y),0,_layer);
        Debug.Log(centerVec+" max "+maxVec);
        
        if(col != null)
        {
  
            hittable = col.GetComponent<IHittable>();
            knockBack = col.GetComponent<IKnockBack>();
            
            Vector2 dir = col.transform.position - transform.position;

            //Debug.Log(_assailCount);
            hittable.GetHit(_attackData.attackDatas[_assailCount-1].damage,_attackData.attackDatas[_assailCount-1].criticalChance,gameObject);

            knockBack.KnockBack(dir,_attackData.attackDatas[_assailCount-1].knockBackValue,(float)_attackData.attackDatas[_assailCount-1].staggerValue*0.01f);
        }

    }
    public void MotionEnd()
    {
        _canAttack = true;
    }
    private void SetAttack()
    {
        _isAttack = true;
        _assailCount++;
        if(_assailCount>=_attackData.assailCount+1)
        {
            return;
        }

        _canAttack = false;
        _attackTimer = _attackData.attackDatas[_assailCount-1].attackTime;
        _agentAnimation.PlayAttackAnimation(_assailCount);
        StopMovement();
    }
    private void StopMovement()
    {
        _agentMovement.StopMove = true;
        _agentAnimation.IsNotChangeFace = true;
    }
    private void PlayMovement()
    {
        _agentMovement.StopMove = false;
        _agentAnimation.IsNotChangeFace = false;
    }
}
