using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpObject : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _distnace;

    private Vector2 curretVae = Vector2.zero;
    private void Start() {
        curretVae = transform.position;
    }

    private void Update() {
        Move();
    }

    private void Move()
    {
        if(_distnace+curretVae.y>transform.position.y)
        {
            transform.position += new Vector3(0,_speed,0) *Time.deltaTime;
            
        }
        else
        {
            
        }
    }
}
