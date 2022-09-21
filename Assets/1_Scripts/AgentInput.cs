using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentInput : MonoBehaviour ,IAgnetInput
{
    [field:SerializeField]
    public UnityEvent<Vector2> OnMovementKeyPress {get;set;}
    [field:SerializeField]
    public UnityEvent<bool,int> OnAttackeyPress {get;set;}
    [field:SerializeField]
    public UnityEvent<float> OnFaceDirection {get;set;}
    [field:SerializeField]
    public UnityEvent<bool> OnJumpKeyPress {get;set;}
    [field:SerializeField]
    public UnityEvent<float> OnRunKeyPress {get;set;}
    [field: SerializeField]
    public UnityEvent OnEvasiveStepKeyPress { get; set; }
    [field: SerializeField]
    public UnityEvent OnParryKeyPress { get; set; }
    private void Update() {
        GetMovementInput();
        GetAttackInput();
        GetFaceDirInput();
        GetJumpInput();
        GetRunInput();
        GetEvasiveStepInput();
        GetParryInput();
    }


    private void GetParryInput()
    {
        if(Input.GetMouseButtonDown(1))
        {
            OnParryKeyPress?.Invoke();
        }
    }
    private void GetMovementInput()
    {
        OnMovementKeyPress?.Invoke(
            new Vector2(Input.GetAxisRaw("Horizontal"), 0)
        );
    }
    private void GetAttackInput()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetMouseButton(0))
            {
                OnAttackeyPress?.Invoke(true, 1);
            }
            else if(Input.GetMouseButtonUp(0))
            {
                OnAttackeyPress?.Invoke(false, 1);
            }
        }
        else
        {
            if(Input.GetMouseButtonDown(0))
            {
                OnAttackeyPress?.Invoke(true, 0);
            }
            else
            {
                OnAttackeyPress?.Invoke(false, 0);
            }
        }
    }
    private void GetFaceDirInput()
    {
        OnFaceDirection?.Invoke(
            Input.GetAxisRaw("Horizontal")
        );
    }
    private void GetJumpInput()
    {
        OnJumpKeyPress?.Invoke(
            Input.GetKeyDown(KeyCode.Space)
        );
    }
    private void GetRunInput()
    {
        OnRunKeyPress?.Invoke(
            Input.GetAxisRaw("Fire3")
        );
    }
    private void GetEvasiveStepInput()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    OnEvasiveStepKeyPress?.Invoke();
        //}
    }

}