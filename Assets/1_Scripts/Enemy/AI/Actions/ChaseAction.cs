using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAction : AIAction
{
    public override void TakeAction()
    {
        Vector2 direction = _enemyBrain.target.position - transform.position;
        _aimovementData.direction = direction.normalized;
        _aimovementData.pointOfInterest = _enemyBrain.target.position - transform.position;

        _enemyBrain.Move(_aimovementData.direction,_aimovementData.pointOfInterest);
    }
}
