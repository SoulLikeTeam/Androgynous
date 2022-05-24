using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackData
{
    public string animationName;
    public Vector2 boxMin,boxMax;
    [Range(1,100)]
    public int damage = 4;
    [Range(0.1f,5f)]
    public float attackTime = 1;
    [Range(0.1f,100)]
    public float criticalChance = 10f;
    [Range(1,100)]
    public int staggerValue = 10;
    [Range(1,100)]
    public int knockBackValue = 10;
}
[CreateAssetMenu(menuName ="SO/Attack/AttackData")]
public class AttackDataSO : ScriptableObject
{
    [Range(0.1f,10)]
    public float afterCastDelay = 1f;
    [Range(1,6)]
    public int assailCount = 1;
    public AttackData[] attackDatas;
    
    
}
