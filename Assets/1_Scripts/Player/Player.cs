using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IAgent, IHittable , IKnockBack
{
    [SerializeField]
    private PlayerStatusSO _playerStatusSO;
    private AgentMovement _agentMovement;
    private PlayerAnimation _playerAnimation;
    private PlayerAttack _playerAttack;

    private bool _isInvincible = false;
    private bool _isDead = false;

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
        if(_isDead||_isInvincible) return;

        Health -= damage;

        HitPoint = damageDealer.transform.position;
        OnGetHit?.Invoke();

        UIManager.Instance.UpdateHpGauge((float)Health/_playerStatusSO.playerMaxHealth);

        if(Health <= 0)
        {
            _isDead = true;
            _playerAnimation.IsNotChangeFace = true;
            UIManager.Instance.PlayDaedAction();
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
        _playerAnimation = GetComponentInChildren<PlayerAnimation>();
        _playerAttack = GetComponent<PlayerAttack>();

        
    }
    private void Start() {
        GameManager.Instance.livePlayer += Die;
        Init();
    }
    private void Init()
    {
        _isDead = false;
        Health = _playerStatusSO.playerMaxHealth;
        UIManager.Instance.UpdateHpGauge(1);
    }
    private void Die()
    {
        Init();
        gameObject.transform.position = GameManager.Instance.SpawnPos;
        _playerAnimation.IsNotChangeFace = false;
        _agentMovement.enabled = true;
        _playerAttack.enabled = true;
    }
    public void InvincibleFrame()
    {
        _isInvincible = true;
        StartCoroutine(InvincibleFrameCoroutine(1f));
    }
    protected IEnumerator InvincibleFrameCoroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _isInvincible = false;
    }
}
