using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class EnemyAttack : Attack
{

    protected Enemy _enemy;
    protected EnemyAiBrain _enemyAiBrain;

    protected bool _waitBeforeNextAttack = false;
    public bool WaitBeforeNextAttack => _waitBeforeNextAttack;

    protected bool _isAttacking;
    public bool IsAttacking => _isAttacking;

    protected LayerMask _layer;

    public UnityEvent AttackFeedback;


    protected override void AwakeChild()
    {
        _enemy = GetComponent<Enemy>();
        _enemyAiBrain = GetComponent<EnemyAiBrain>();
        _layer = LayerMask.GetMask("Player");
    }
    public Transform GetTarget()
    {
        return _enemyAiBrain.target;
    }
    protected IEnumerator WaitBeforeAttackCoroutine(float waitForTime)
    {
        _waitBeforeNextAttack = true;
        yield return new WaitForSeconds(waitForTime);
        _waitBeforeNextAttack = false;
    }

    public override abstract void OnAttack(int mode);
}
