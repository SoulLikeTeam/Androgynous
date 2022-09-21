using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/Enemies/EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    public string enemyName;
    public GameObject prefab;
    public int maxHealth;
    public GameObject damagedEffect;

    public bool neutralActive;
    
}
