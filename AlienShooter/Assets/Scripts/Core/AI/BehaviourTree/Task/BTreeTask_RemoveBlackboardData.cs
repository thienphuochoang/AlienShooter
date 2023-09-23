using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTreeTask_RemoveBlackboardData : BTreeNode
{
    private BehaviourTree _behaviourTree;
    private string _keyToRemove;
    public BTreeTask_RemoveBlackboardData(BehaviourTree behaviourTree, string keyToRemove)
    {
        _behaviourTree = behaviourTree;
        _keyToRemove = keyToRemove;
    }

    protected override NodeResult Execute()
    {
        if (_behaviourTree != null && _behaviourTree.Blackboard != null)
        {
            _behaviourTree.Blackboard.RemoveData(_keyToRemove);
            return NodeResult.Success;
        }

        return NodeResult.Failure;
    }
}
