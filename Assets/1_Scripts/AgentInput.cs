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
    private void Update() {
        GetMovementInput();
        GetAttackInput();
        GetFaceDirInput();
        GetJumpInput();
        GetRunInput();
    }
    private void GetMovementInput()
    {
        OnMovementKeyPress?.Invoke(
            new Vector2(Input.GetAxisRaw("Horizontal"), 0)
        );
    }
    private void GetAttackInput()
    {
        OnAttackeyPress?.Invoke(
            Input.GetButtonDown("Fire1"),0
        );
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
            Input.GetButtonDown("Jump")
        );
    }
    private void GetRunInput()
    {
        OnRunKeyPress?.Invoke(
            Input.GetAxisRaw("Fire3")
        );
    }

}