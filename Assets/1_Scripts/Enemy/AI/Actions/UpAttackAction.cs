using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAttackAction : AIAction
{
    public override void TakeAction()
    {
        _aimovementData.direction = Vector2.zero;
        _aimovementData.pointOfInterest = _enemyBrain.target.position;

         _enemyBrain.Move(_aimovementData.direction,_aimovementData.pointOfInterest);
        _aiActionData.attack = true;

        _enemyBrain.Attack(2);

    }
}