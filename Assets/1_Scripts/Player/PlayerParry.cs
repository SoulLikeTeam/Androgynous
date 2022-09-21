using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerParry : MonoBehaviour
{

    [SerializeField]
    private float _parryTime; //�и��� �󸶳� Ȱ��ȭ ��ų��
    [SerializeField]
    private float _waitForTime; //�ٽ� Ȱ��ȭ ��ų�� ������ �ð�

    [SerializeField]
    private GameObject _effect;

    private float _time = 0; //�и� Ÿ�̹��� ����� ����

    private bool _isParry = false; //�и��� Ȱ��ȭ �� �� �ִ��� ������
    private bool _isActiveParry = false; //�и��� Ȱ��ȭ �Ǿ� �ִ��� �ƴ���

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
            Debug.Log("�и� Ȱ��ȭ!!!!");
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
            Debug.Log("�и�Ÿ�� : " + _time);
            _isActiveParry = true;
        }
        else
        {
            _playerAnimation.WaitParry(false);
            Debug.Log("����");
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
            Debug.Log("�и�!!!");
            return true;
        }
        else
        {
            Debug.Log("�� �ƴϾ�!!!");
            return false;
        }
    }
    private void ParryEffectiveToDealer(GameObject dealer)
    {
        Enemy enemy = dealer.GetComponent<Enemy>();

        enemy.NeutralEnemy();
    }
}
