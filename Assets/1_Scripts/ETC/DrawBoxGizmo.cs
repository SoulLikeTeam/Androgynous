using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBoxGizmo : MonoBehaviour
{
    [SerializeField]
    Vector2 minVec = Vector2.one, maxVec = Vector2.one;

    private void OnDrawGizmos() {
        Vector2 centerVec = new Vector2(maxVec.x-minVec.x,maxVec.y-minVec.y) * 0.5f;
        Gizmos.matrix = transform.localToWorldMatrix;  
        Gizmos.color = Color.green;  
        Gizmos.DrawWireCube(centerVec, maxVec);
    }
}
