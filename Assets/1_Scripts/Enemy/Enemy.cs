using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour ,IAgent ,IHittable ,IKnockBack
{
    [SerializeField]
    private EnemyDataSO _enemyData;
    public EnemyDataSO EnemyData => _enemyData;

    private bool _isDead = false;
    private AgentMovement _agentMovement;
    private EnemyAnimation _enemyAnimation;
    private EnemyAttack _enemyAttack;

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
        float critical = Random.Range(1,100);
        bool isCritial = false;

        if(critical<criticalChance)
        {
            isCritial = true;
            damage = damage * 2;
        }

        Health -= damage;
        HitPoint = damageDealer.transform.position;

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
    }
    public void PerformAttack(bool value,int mode)
    {
        if(!_isDead)
        {
            _enemyAttack.OnAttack(mode);
        }
    }
    private void Start() {
        Health = _enemyData.maxHealth;
    }

    
}
