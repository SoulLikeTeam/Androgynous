using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : Attack
{
    private AgentMovement _agentMovement = null;
    protected PlayerAnimation _playerAnimation = null;

    protected float _attackBeforeMoveTimer = 0;

    private float _heavyAttackTime = 0;

    protected bool _isInput = false;
    protected bool _isHeavyAttack = false;
    protected bool _isCritical = false;
    protected bool _isAttackBeforeMovement = false;

    private LayerMask _layer;

    protected override void Awake()
    {
        _playerAnimation = GetComponentInChildren<PlayerAnimation>();
        _agentMovement = GetComponent<AgentMovement>();
        _layer = LayerMask.GetMask("Enemy");
    }
    private void Update() {
        OnAttack(0);
        HeavyAttack();
    }
    public void IsInputAttackKey(bool value,int mode)
    {
        if(mode==0)
        {
            _isInput = value;
            _isHeavyAttack = false;
        }
        else if(mode==1)
        {
            _isHeavyAttack = value;
            _isInput = false;
        }
       
    }
    protected void HeavyAttack()
    {
        if(_isHeavyAttack)
        {
            //Debug.Log("Â÷Â¡!!!");
            _playerAnimation.HeavyAttack();
            _heavyAttackTime += Time.deltaTime;
            StopMovement();
        }
        else if(_heavyAttackTime > 1)
        {
            Debug.Log("°­ °ø°Ý!!");
            _heavyAttackTime = 0;
            _isCritical = true;
            SetAttack(3);
        }
        else
        {
            PlayMovement();
            _heavyAttackTime = 0;
        }
    }
    public override void OnAttack(int mode)
    {
        if(!_agentMovement.IsJump) return;
        if(_isInput&&!_isAttack&&Time.time >= _lastAttackTime + _attackData.afterCastDelay)
        {
            SetAttack(0);
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
                _playerAnimation.StopAttackAnimation();
                PlayMovement();
            }
            else if(_isInput&&_canAttack)
            {
                SetAttack(0);
            }

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
        //Debug.Log(centerVec+" max "+maxVec);
        
        if(col != null)
        {
  
            hittable = col.GetComponent<IHittable>();
            knockBack = col.GetComponent<IKnockBack>();
            
            Vector2 dir = col.transform.position - transform.position;

            //Debug.Log(_assailCount);
            if(_isCritical)
            {
                hittable.GetHit((int)(_attackData.attackDatas[_assailCount - 1].damage*2f), _attackData.attackDatas[_assailCount - 1].criticalChance, gameObject);
            }
            else
            {
                hittable.GetHit(_attackData.attackDatas[_assailCount - 1].damage, _attackData.attackDatas[_assailCount - 1].criticalChance, gameObject);
            }
            

            knockBack.KnockBack(dir,_attackData.attackDatas[_assailCount-1].knockBackValue,(float)_attackData.attackDatas[_assailCount-1].staggerValue*0.01f);
        }

    }
    public void MotionEnd()
    {
        _isCritical = false;
        _canAttack = true;
    }
    private void SetAttack(int type)
    {
        float foce = 8;
        if(type>0)
        {
            foce = 15;
            _isAttack = true;
            _assailCount = type;
        }
        else
        {
            _isAttack = true;
            _isInput = false;
            _assailCount++;
        }
        if (_assailCount >= _attackData.assailCount+1)
        {
            return;
        }

        _agentMovement.AddFoceMovement(foce, 0.2f);
        _canAttack = false;
        _attackTimer = _attackData.attackDatas[_assailCount - 1].attackTime;
        _playerAnimation.PlayAttackAnimation(_assailCount);
        StopMovement();
    }
    private void StopMovement()
    {
        _agentMovement.StopMove = true;
        _playerAnimation.IsNotChangeFace = true;
    }
    private void PlayMovement()
    {
        _agentMovement.StopMove = false;
        _playerAnimation.IsNotChangeFace = false;
    }
}
