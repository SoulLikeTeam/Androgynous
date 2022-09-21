using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDecision : AIDecision
{
    public Vector2 distanceMeasurementVec;
    public override bool MakeADecision()
    {
        Vector2 vec =  _enemyBrain.target.position - transform.position;
        vec.x = Mathf.Abs(vec.x);
        
        if(vec.y>distanceMeasurementVec.y)
        {   
            //Debug.Log(vec);
            return true;
        }
        return false;
    }

    #if UNITY_EDITOR

    private void OnDrawGizmos()
    {

    }

#endif
}
