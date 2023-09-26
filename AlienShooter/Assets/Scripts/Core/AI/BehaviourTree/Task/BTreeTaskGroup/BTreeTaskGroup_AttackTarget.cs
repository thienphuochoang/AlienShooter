using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTreeTaskGroup_AttackTarget : BTreeTask_Group
{
    private float attackDistance = 1.3f;
    private float rotateDegrees = 10f;
    private float waitingTime = 3.5f;
    public BTreeTaskGroup_AttackTarget(BehaviourTree tree, 
        float inputAttackDistance = 1.3f, 
        float inputRotateDegrees = 10f, 
        float inputWaitingTime = 3.5f) : base(tree)
    {
        attackDistance = inputAttackDistance;
        rotateDegrees = inputRotateDegrees;
        waitingTime = inputWaitingTime;
    }

    protected override void ConstructTree(out BTreeNode root)
    {
        Sequencer attackTargetSequencer = new Sequencer();
        BTreeTask_MoveToTarget moveToTargetTask = new BTreeTask_MoveToTarget(_behaviourTree, "Target", attackDistance);
        BTreeTask_RotateTowardsTarget rotateTowardsTargetTask = new BTreeTask_RotateTowardsTarget(_behaviourTree, "Target", rotateDegrees);
        BTreeTask_Attack attackTask = new BTreeTask_Attack(_behaviourTree, "Target");
        BTreeTask_Wait waitAttackTask = new BTreeTask_Wait(waitingTime);
        attackTargetSequencer.AddChild(moveToTargetTask);
        attackTargetSequencer.AddChild(rotateTowardsTargetTask);
        attackTargetSequencer.AddChild(attackTask);
        attackTargetSequencer.AddChild(waitAttackTask);
        BlackboardDecorator attackTargetDecorator = new BlackboardDecorator(_behaviourTree,
            attackTargetSequencer,
            "Target",
            BlackboardDecorator.RunCondition.KeyExist,
            BlackboardDecorator.NotifyRule.RunConditionChange,
            BlackboardDecorator.NotifyAbort.Both);
        root = attackTargetDecorator;
    }
}
