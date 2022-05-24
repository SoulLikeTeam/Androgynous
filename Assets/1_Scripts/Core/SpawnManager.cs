using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _spawnTrm = new List<GameObject>();

    private void Start() {
        SpawnEnemy();
        GameManager.Instance.SpawnEnemy += SpawnEnemy;
    }
    public void SpawnEnemy()
    {
        foreach(GameObject gb in _spawnTrm)
        {
            PoolableMono ob;
            ob = PoolManager.Instance.Pop(gb.name);
            ob.gameObject.transform.position = gb.transform.position;
        }
    }

    //public void 
}
