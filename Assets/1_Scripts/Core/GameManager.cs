using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private PoolingListSO _initList = null;

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

    private void Awake() 
    {
        if (Instance != null)
            Debug.LogError("Multiple GameManager is running");
        Instance = this;

        PoolManager.Instance =  new PoolManager(transform);

        CreatePool();
    }

    private void CreatePool()
    {
        foreach (PoolingPair pair in _initList.list)
            PoolManager.Instance.CreatePool(pair.prefab, pair.poolCnt);
    }
}
