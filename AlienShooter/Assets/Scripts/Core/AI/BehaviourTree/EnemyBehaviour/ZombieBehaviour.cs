using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehaviour : BehaviourTree
{
    protected override void ConstructTree(out BTreeNode rootNode)
    {
        Selector rootSelector = new Selector();
        #region Attack State

        BTreeTaskGroup_AttackTarget attackTargetTaskGroup = new BTreeTaskGroup_AttackTarget(this, 1.3f, 10f, 3.5f);
        /*Sequencer attackTargetSequencer = new Sequencer();
        BTreeTask_MoveToTarget moveToTargetTask = new BTreeTask_MoveToTarget(this, "Target", 1.3f);
        BTreeTask_RotateTowardsTarget rotateTowardsTargetTask = new BTreeTask_RotateTowardsTarget(this, "Target", 10f);
        BTreeTask_Attack attackTask = new BTreeTask_Attack(this, "Target");
        BTreeTask_Wait waitAttackTask = new BTreeTask_Wait(3.5f);
        attackTargetSequencer.AddChild(moveToTargetTask);
        attackTargetSequencer.AddChild(rotateTowardsTargetTask);
        attackTargetSequencer.AddChild(attackTask);
        attackTargetSequencer.AddChild(waitAttackTask);
        BlackboardDecorator attackTargetDecorator = new BlackboardDecorator(this,
            attackTargetSequencer,
            "Target",
            BlackboardDecorator.RunCondition.KeyExist,
            BlackboardDecorator.NotifyRule.RunConditionChange,
            BlackboardDecorator.NotifyAbort.Both);
        rootSelector.AddChild(attackTargetDecorator);*/
        rootSelector.AddChild(attackTargetTaskGroup);
        #endregion

        #region Check Last Seen Location State

        BTreeTaskGroup_MoveToLastSeenLocation moveToLastSeenLocationTaskGroup =
            new BTreeTaskGroup_MoveToLastSeenLocation(this, 0.1f, 15f);
        /*Sequencer checkLastSeenSequencer = new Sequencer();
        BTreeTask_MoveToLocation moveToLastSeenTask = new BTreeTask_MoveToLocation(this, "LastSeenLocation", 0.1f);
        BTreeTask_Wait waitAtLastSeenTask = new BTreeTask_Wait(15f);
        BTreeTask_RemoveBlackboardData removeLastSeenLocationBlackboardDataTask =
            new BTreeTask_RemoveBlackboardData(this, "LastSeenLocation");
        checkLastSeenSequencer.AddChild(moveToLastSeenTask);
        checkLastSeenSequencer.AddChild(waitAtLastSeenTask);
        checkLastSeenSequencer.AddChild(removeLastSeenLocationBlackboardDataTask);

        BlackboardDecorator checkLastSeenLocationDecorator = new BlackboardDecorator(this,
            checkLastSeenSequencer,
            "LastSeenLocation",
            BlackboardDecorator.RunCondition.KeyExist,
            BlackboardDecorator.NotifyRule.RunConditionChange,
            BlackboardDecorator.NotifyAbort.None);
        rootSelector.AddChild(checkLastSeenLocationDecorator);*/
        rootSelector.AddChild(moveToLastSeenLocationTaskGroup);
        #endregion
        
        #region Wandering State

        BTreeTaskGroup_Wandering wanderingGroupTask = new BTreeTaskGroup_Wandering(this, 0.1f, 10f);
        /*Sequencer wanderingSequencer = new Sequencer();
        BTreeTask_GetNextWanderingLocation getNextWanderingLocationTask =
            new BTreeTask_GetNextWanderingLocation(this, "WanderPoint");
        BTreeTask_MoveToLocation moveToLocationTask = new BTreeTask_MoveToLocation(this, "WanderPoint", 0.1f);
        BTreeTask_Wait waitTask = new BTreeTask_Wait(10f);
        wanderingSequencer.AddChild(getNextWanderingLocationTask);
        wanderingSequencer.AddChild(moveToLocationTask);
        wanderingSequencer.AddChild(waitTask);
        rootSelector.AddChild(wanderingSequencer);*/
        rootSelector.AddChild(wanderingGroupTask);
        #endregion
        
        rootNode = rootSelector;
    }
}
