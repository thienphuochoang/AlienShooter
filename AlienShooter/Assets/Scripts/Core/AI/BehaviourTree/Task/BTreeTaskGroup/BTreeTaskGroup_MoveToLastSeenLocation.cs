using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTreeTaskGroup_MoveToLastSeenLocation : BTreeTask_Group
{
    private float _stopDistance;
    private float _waitingTime;
    public BTreeTaskGroup_MoveToLastSeenLocation(BehaviourTree tree, float stopDistance = 0.1f, float waitingTime = 15f) : base(tree)
    {
        _stopDistance = stopDistance;
        _waitingTime = waitingTime;
    }

    protected override void ConstructTree(out BTreeNode root)
    {
        Sequencer checkLastSeenSequencer = new Sequencer();
        BTreeTask_MoveToLocation moveToLastSeenTask = new BTreeTask_MoveToLocation(_behaviourTree, "LastSeenLocation", _stopDistance);
        BTreeTask_Wait waitAtLastSeenTask = new BTreeTask_Wait(_waitingTime);
        BTreeTask_RemoveBlackboardData removeLastSeenLocationBlackboardDataTask =
            new BTreeTask_RemoveBlackboardData(_behaviourTree, "LastSeenLocation");
        checkLastSeenSequencer.AddChild(moveToLastSeenTask);
        checkLastSeenSequencer.AddChild(waitAtLastSeenTask);
        checkLastSeenSequencer.AddChild(removeLastSeenLocationBlackboardDataTask);

        BlackboardDecorator checkLastSeenLocationDecorator = new BlackboardDecorator(_behaviourTree,
            checkLastSeenSequencer,
            "LastSeenLocation",
            BlackboardDecorator.RunCondition.KeyExist,
            BlackboardDecorator.NotifyRule.RunConditionChange,
            BlackboardDecorator.NotifyAbort.None);
        root = checkLastSeenLocationDecorator;
    }
}
