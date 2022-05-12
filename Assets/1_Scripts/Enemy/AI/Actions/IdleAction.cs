using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : AIAction
{
    public override void TakeAction()
    {
        _aimovementData.direction = Vector2.zero;
        _aimovementData.pointOfInterest = transform.position;
        _enemyBrain.Move(_aimovementData.direction,_aimovementData.pointOfInterest);
    }
}
