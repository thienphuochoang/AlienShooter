using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBehaviourTree
{
    public void RotateTowards(GameObject target);
    public void Attack(GameObject target);
    public void TriggerWalkAnimation();
    public void CancelWalkAnimation();
    public void TriggerRunAnimation();
    public void CancelRunAnimation();
}
