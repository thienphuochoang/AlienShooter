using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehaviour : BehaviourTree
{
    protected override void ConstructTree(out BTreeNode rootNode)
    {
        BTreeTask_MoveToTarget moveToTargetTask = new BTreeTask_MoveToTarget(this, "Target", 1f);
        rootNode = moveToTargetTask;
    }
}
