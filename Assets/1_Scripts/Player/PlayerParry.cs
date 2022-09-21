using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerParry : MonoBehaviour
{

    [SerializeField]
    private float _parryTime; //패리를 얼마나 활성화 시킬지
    [SerializeField]
    private float _waitForTime; //다시 활성화 시킬떄 까지의 시간

    [SerializeField]
    private GameObject _effect;

    private float _time = 0; //패리 타이밍을 계산할 변수

    private bool _isParry = false; //패리를 활성화 할 수 있는지 없는지
    private bool _isActiveParry = false; //패리가 활성화 되어 있는지 아닌지

    private PlayerAnimation _playerAnimation;
    private void Awake()
    {
        _playerAnimation = GetComponentInChildren<PlayerAnimation>();
    }
    private void Update()
    {
        Parry();
    }
    private void Init()
    {
        _isParry = false;
        _isActiveParry = false;
        _playerAnimation.WaitParry(false);
        _time = 0;
    }
    public void OnParry()
    {
        if(!_isParry)
        {
            Debug.Log("패리 활성화!!!!");
            _isParry = true;
            _playerAnimation.WaitParry(true);
            _playerAnimation.ParryAniamtion();
            StartCoroutine(WaitBeforeTimeParry(_waitForTime));
        }
    }
    private void Parry()
    {
        if (!_isParry) return;

        if(_time<_parryTime)
        {
            
            _time += Time.deltaTime;
            Debug.Log("패리타임 : " + _time);
            _isActiveParry = true;
        }
        else
        {
            _playerAnimation.WaitParry(false);
            Debug.Log("히히");
            _isActiveParry = false;
        }
    }

    private IEnumerator WaitBeforeTimeParry(float time)
    {
        yield return new WaitForSeconds(time);
        Init();
    }

    public bool CheckParryPoint(GameObject dealer)
    {
        if (_isActiveParry)
        {
            GameObject ob = Instantiate(_effect, transform);
            ob.transform.position = transform.position;
            ParryEffectiveToDealer(dealer);
            Init();
            Debug.Log("패리!!!");
            return true;
        }
        else
        {
            Debug.Log("응 아니야!!!");
            return false;
        }
    }
    private void ParryEffectiveToDealer(GameObject dealer)
    {
        Enemy enemy = dealer.GetComponent<Enemy>();

        enemy.NeutralEnemy();
    }
}
