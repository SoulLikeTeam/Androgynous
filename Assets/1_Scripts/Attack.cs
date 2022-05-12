using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public abstract class Attack : MonoBehaviour
{
    [SerializeField]
    protected AttackDataSO _attackData;
    public AttackDataSO AttackData => _attackData;

    protected AgentAnimation _agentAnimation;

    protected float _lastAttackTime = 0;
    protected float _attackTimer = 0;
    protected int _assailCount = 0;

    protected bool _canAttack = true;
    protected bool _isAttack = true;
    private void Awake() {
        _agentAnimation = GetComponentInChildren<AgentAnimation>();

        AwakeChild();
    }

    protected virtual void AwakeChild()
    {
        //no
    }

    private void Start() {
        _lastAttackTime = Time.time;
    }
    public abstract void OnAttack(int mode);
}
