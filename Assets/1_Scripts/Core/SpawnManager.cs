using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _spawnTrm = new List<Transform>();

    private void Start() {
        SpawnEnemy();
        GameManager.Instance.SpawnEnemy += SpawnEnemy;
    }
    public void SpawnEnemy()
    {
        foreach(Transform trm in _spawnTrm)
        {
            PoolableMono ob;
            ob = PoolManager.Instance.Pop("Enemy");
            ob.gameObject.transform.position = trm.position;
        }
    }

    //public void 
}
