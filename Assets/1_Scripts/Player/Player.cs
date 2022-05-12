using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IAgent, IHittable , IKnockBack
{
    private AgentMovement _agentMovement;
    private PlayerAnimation _playerAnimation;
    private PlayerAttack _playerAttack;
    #region Interface
    public int Health {get; private set;}

    [field:SerializeField]
    public UnityEvent OnDie {get;set;}
    [field:SerializeField]
    public UnityEvent OnGetHit {get;set;}

    public bool IsEnemy => false;

    public Vector3 HitPoint {get; private set;}

    public void GetHit(int damage, float criticalChance, GameObject damageDealer)
    {
        Health -= damage;

        HitPoint = damageDealer.transform.position;
        OnGetHit?.Invoke();

        if(Health <= 0)
        {
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
        _playerAnimation = GetComponent<PlayerAnimation>();
        _playerAttack = GetComponent<PlayerAttack>();

        Health = 100;
    }
}
