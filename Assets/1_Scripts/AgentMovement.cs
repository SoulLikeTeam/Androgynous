using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentMovement : MonoBehaviour
{
    private Rigidbody2D _rigid = null;
    [SerializeField]
    private Transform _JumpCheckTrm = null;
    [SerializeField]
    private MovementDataSO _movementSO;

    [field:SerializeField]
    public UnityEvent<float,bool> OnVelocityChange { get; private set; } //애니메이션 정보넘기는 이벤트

    private Coroutine _knockBackCo = null;

    protected float _currentVelocity = 3;
    protected float _addVelocity = 1;
    protected Vector2 _movementDirection;
    public Vector2 MovementDirection => _movementDirection;

#region 상태 체크
    protected bool _isJump = false;
    public bool IsJump{get{return _isJump;}private set{}}
    protected bool _stopMove = false; 
    public bool StopMove 
    {
        get
        {
            return _stopMove;
        }
        set
        {
            _stopMove = value;
        }
    }
    protected bool _isKnockBack = false;
#endregion

    private LayerMask _layer;

    private void Awake() {
        _rigid = GetComponent<Rigidbody2D>();
        _layer = LayerMask.GetMask("Wall"); 
    }
    public void MoveAgent(Vector2 movementInput)
    {
        if(movementInput.sqrMagnitude > 0)
        {
            if(Vector2.Dot(movementInput, _movementDirection) < 0)
            {
                _currentVelocity = 0;
            }
            _movementDirection = movementInput.normalized;
        }
        _currentVelocity = CalculateSpeed(movementInput);
    }
    private float CalculateSpeed(Vector2 movementInput)
    {
        if(movementInput.sqrMagnitude > 0)
        {
            _currentVelocity += _movementSO.acceleration * Time.deltaTime;
        }else
        {
            _currentVelocity -= _movementSO.deAcceleration * Time.deltaTime;
        }

        return Mathf.Clamp(_currentVelocity, 0, _movementSO.maxSpeed*_addVelocity);
    }
    public void JumpAgent(bool isjump)
    {
        if(StopMovement())return;
        if(isjump&&_isJump==true)
        {
            _rigid.AddForce(Vector2.up * _movementSO.jumpForce,ForceMode2D.Impulse);
            _rigid.gravityScale = _movementSO.jumpGravity;
        }
        
        JumpCheckCollider();
    }

    private void JumpCheckCollider()
    {
        if(Physics2D.OverlapCircle(_JumpCheckTrm.position,_movementSO.jumpCheckRadius,_layer))
        {
            _isJump = true;
            _rigid.gravityScale = 1f;
        }
        else
        {
            _isJump = false;
            _rigid.gravityScale = _movementSO.jumpGravity;
        }
    }

    public void RunAgent(float addSpeed)
    {
        if(addSpeed>0&&_isJump)
        {
            _addVelocity = 1.5f;
        }
        else
        {
            _addVelocity = 1;
        }
    }

    private void FixedUpdate()
    {
        if(StopMovement())return;
        OnVelocityChange?.Invoke(_currentVelocity,_addVelocity!=1);
        if(!_isKnockBack)
            _rigid.velocity = new Vector2(_movementDirection.x*_currentVelocity,Mathf.Clamp(_rigid.velocity.y,-_movementSO.jumpForce,_movementSO.jumpForce));
    }
    public void StopImmediatelly()
    {
        _currentVelocity = 0;
        _rigid.velocity = Vector2.zero;
    }
    protected bool StopMovement()
    {
        if(_stopMove&&!_isKnockBack)
        {
            _currentVelocity = 0;
            _rigid.velocity = Vector2.zero;
            return true;
        }
        else if(_stopMove)
        {
            return true;
        }
        return false;
    }
    public void KnockBack(Vector2 direction, float power, float duration)
    {
        if(!_isKnockBack)
        {
            _isKnockBack = true;
            _knockBackCo = StartCoroutine(KnockBackCoroutine(direction, power, duration));
        }
    }
    
    public void ResetKnockBack()
    {
        if(_knockBackCo != null)
        {
            StopCoroutine(_knockBackCo);
        }
        ResetKnockBackParam(); 
    }

    IEnumerator KnockBackCoroutine(Vector2 direction, float power, float duration)
    {
        direction.Normalize();
        _rigid.AddForce(new Vector2(direction.x ,0) * power, ForceMode2D.Impulse);
        yield return new WaitForSeconds(duration);
        ResetKnockBackParam();
    }

    private void ResetKnockBackParam()
    {
        _currentVelocity = 0;
        _rigid.velocity = Vector2.zero;
        _isKnockBack = false;
    }
}
