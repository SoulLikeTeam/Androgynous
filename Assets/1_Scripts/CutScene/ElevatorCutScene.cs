using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorCutScene : MonoBehaviour
{
    [SerializeField]
    private GameObject _elevatorBackGround;
    [SerializeField]
    private GameObject _elevatorColor;
    [SerializeField]
    private UpObject _elevatorDoor;
    [SerializeField]
    private GameObject _light;
    private void Start() {
        OnEvent();
    }

    private void OnEvent()
    {
        _elevatorColor.SetActive(true);
        StartCoroutine(Timer(2,_elevatorBackGround,false));
        StartCoroutine(OpenDoor(4));
        StartCoroutine(Timer(6,_light,true));

    }
    IEnumerator OpenDoor(float duration)
    {
        yield return new WaitForSeconds(duration);
        _elevatorDoor.enabled = true;
    }
    IEnumerator Timer(float duration,GameObject ob,bool value)
    {
        Debug.Log("asd");
        yield return new WaitForSeconds(duration);
        ob.SetActive(value);
    }
}
