using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IAgnetInput
{
    public UnityEvent<Vector2> OnMovementKeyPress {get;set;}
    public UnityEvent<float> OnFaceDirection {get;set;}
    public UnityEvent<bool,int> OnAttackeyPress {get;set;}
    public UnityEvent<bool> OnJumpKeyPress {get;set;}
    public UnityEvent<float> OnRunKeyPress {get;set;}
}
