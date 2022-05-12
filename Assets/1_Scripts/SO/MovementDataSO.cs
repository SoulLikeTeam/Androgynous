using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/Agent/MovementData")]
public class MovementDataSO : ScriptableObject
{
    [Range(1, 10)]
    public float maxSpeed = 5;
    [Range(1,30)]
    public float jumpForce = 10;
    [Range(0.1f,5f)]
    public float jumpCheckRadius = 0.5f;

    [Range(0.1f, 100f)]
    public float acceleration = 50, deAcceleration = 50;
    [Range(0.1f,10)]
    public float jumpGravity = 5f;
}
