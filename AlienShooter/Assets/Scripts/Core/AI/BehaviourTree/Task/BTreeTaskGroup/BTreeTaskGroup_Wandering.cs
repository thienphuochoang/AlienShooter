using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTreeTaskGroup_Wandering : BTreeTask_Group
{
    private float _stopDistance;
    private float _waitingTime;
    public BTreeTaskGroup_Wandering(BehaviourTree tree, float stopDistance = 0.1f, float waitingTime = 10f) : base(tree)
    {
        _stopDistance = stopDistance;
        _waitingTime = waitingTime;
    }

    protected override void ConstructTree(out BTreeNode root)
    {
        Sequencer wanderingSequencer = new Sequencer();
        BTreeTask_GetNextWanderingLocation getNextWanderingLocationTask =
            new BTreeTask_GetNextWanderingLocation(_behaviourTree, "WanderPoint");
        BTreeTask_MoveToLocation moveToLocationTask = new BTreeTask_MoveToLocation(_behaviourTree, "WanderPoint", _stopDistance);
        BTreeTask_Wait waitTask = new BTreeTask_Wait(_waitingTime);
        wanderingSequencer.AddChild(getNextWanderingLocationTask);
        wanderingSequencer.AddChild(moveToLocationTask);
        wanderingSequencer.AddChild(waitTask);
        root = wanderingSequencer;
    }
}
