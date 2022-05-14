using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScrolling : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Transform[] backgrounds;
 
    float leftPosY = 0f;
    float rightPosY = 0f;
    float xScreenHalfSize;
    float yScreenHalfSize;
    void Start()
    {
        xScreenHalfSize = Camera.main.orthographicSize;
        yScreenHalfSize = yScreenHalfSize * Camera.main.aspect;
 
        leftPosY = (xScreenHalfSize * 2);
        rightPosY = xScreenHalfSize * 2 * backgrounds.Length;
    }

    // Update is called once per frame
    void Update()
    {
       for(int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].position += new Vector3(0, speed, 0) * Time.deltaTime;
 
            if(backgrounds[i].position.y > leftPosY)
            {
                Vector3 nextPos = backgrounds[i].position;
                nextPos = new Vector3(nextPos.x , nextPos.y - rightPosY, nextPos.z);
                backgrounds[i].position = nextPos;
            }
        }
    }
}
