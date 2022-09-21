using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : PoolableMono ,IAgent ,IHittable ,IKnockBack
{
    [SerializeField]
    private EnemyDataSO _enemyData;
    public EnemyDataSO EnemyData => _enemyData;

    private bool _isDead = false;
    private bool _isNeutral = false;

    private AgentMovement _agentMovement;
    private EnemyAnimation _enemyAnimation;
    private EnemyAttack _enemyAttack;
    private AIActionData _aiActionData;

    #region 인테페이스

    public int Health {get;private set;}

    [field:SerializeField]
    public UnityEvent OnDie {get;set;}
    [field:SerializeField]
    public UnityEvent OnGetHit {get;set;}

    public bool IsEnemy => true;
    public Vector3 HitPoint {get;private set;}
    public void GetHit(int damage,float criticalChance,GameObject damageDealer)
    {
       if(_isDead) return;
        if(_enemyData.neutralActive)
            if (!_isNeutral && _aiActionData.attack == false)
            {
                _enemyAnimation.GuardAnimation();
                return;
            }
        float critical = Random.Range(1,100);
        //bool isCritial = false;

        if(critical<criticalChance)
        {
            //isCritial = true;
            damage = damage * 2;
        }

        Health -= damage;
        HitPoint = damageDealer.transform.position;

        GameObject ob = Instantiate(_enemyData.damagedEffect, transform);
        ob.transform.position = transform.position;
        

        OnGetHit?.Invoke();

        if(Health<=0)
        {
            Debug.Log("Dead");
            _isDead = true;
            _agentMovement.StopImmediatelly();
            _agentMovement.enabled = false;
            OnDie?.Invoke();
        }
    }

    public void KnockBack(Vector2 direction, float power, float duration)
    {
        _agentMovement.KnockBack(direction,power,duration);
    }
    #endregion

    private void Awake() {
        _agentMovement = GetComponent<AgentMovement>();
        _enemyAttack = GetComponent<EnemyAttack>();
        _enemyAnimation = GetComponentInChildren<EnemyAnimation>();
        _aiActionData = transform.Find("AI").GetComponent<AIActionData>();
    }
    private void Start() {
        GameManager.Instance.livePlayer += Die;
        Health = _enemyData.maxHealth;
    }
    private void OnDisable()
    {
        Reset();
    }
    public void NeutralEnemy()
    {
        _isNeutral = true;
        Debug.Log("무력화 ㅠㅠ");
        StartCoroutine(WaitForTimeNeutral(3));
    }
    private IEnumerator WaitForTimeNeutral(float time)
    {
        yield return new WaitForSeconds(time);
        _isNeutral = false;
    }

    public void PerformAttack(bool value,int mode)
    {
        if(!_isDead)
        {
            _enemyAttack.OnAttack(mode);
        }
    }
    public override void Reset()
    {
        Health = _enemyData.maxHealth;
        _isDead = false;
        _isNeutral = false;
        _agentMovement.enabled = true;
        _enemyAttack.Reset();
        _enemyAnimation.LiveEnemy();
    }

    public void Die()
    {
        PoolManager.Instance.Push(this);
    }
}
