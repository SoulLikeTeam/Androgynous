using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private PoolingListSO _initList = null;

    [SerializeField]
    private RectTransform fade;

    public Action livePlayer {get;set;}
    public Action SpawnEnemy {get;set;}

    [field:SerializeField]
    public Vector3 SpawnPos {get;} = new Vector3(-9.44f,-3.18f,0);

    public bool IsEvent;

    private Transform _playerTrm;
    public Transform PlayerTrm
    {
        get
        {
            if(_playerTrm == null)
            {
                _playerTrm = GameObject.FindWithTag("Player").transform;
            }
            return _playerTrm;
        }
    }   

    [SerializeField]
    private GameObject _gameMenu = null;

    private void Awake() 
    {
        if (Instance != null)
            Debug.LogError("Multiple GameManager is running");
        Instance = this;


        PoolManager.Instance =  new PoolManager(transform);
        UIManager.Instance = new UIManager();
        
        CreatePool();
    }

    private void Start() {
        _gameMenu.SetActive(false);
    }
    
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!_gameMenu.activeSelf)
            {
                _gameMenu.SetActive(true);
            }
            else
            {
                _gameMenu.SetActive(false);
            }
        }
    }
    public void FadeIn()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(fade.DOAnchorPosY(0, 1.5f));
    }
    public void FadeOut()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(fade.DOAnchorPosY(-1050, 1.5f));
    }
    private void CreatePool()
    {
        foreach (PoolingPair pair in _initList.list)
            PoolManager.Instance.CreatePool(pair.prefab, pair.poolCnt);
    }

    //코루틴 메니저
    public void CallWaitForOneFrame(Action act) { StartCoroutine(DoCallWaitForOneFrame(act)); } 
    public void CallWaitForSeconds(float seconds, Action act) { 
        StopAllCoroutines();
        StartCoroutine(DoCallWaitForSeconds(seconds, act)); 
    } 
    private IEnumerator DoCallWaitForOneFrame(Action act) { yield return 0; act(); } 
    private IEnumerator DoCallWaitForSeconds(float seconds, Action act) { yield return new WaitForSeconds(seconds); act();}
}
