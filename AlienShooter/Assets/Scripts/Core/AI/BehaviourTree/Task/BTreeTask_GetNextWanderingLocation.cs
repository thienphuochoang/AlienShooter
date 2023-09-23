using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTreeTask_GetNextWanderingLocation : BTreeNode
{
    private WanderBehaviour _wanderBehaviour;
    private BehaviourTree _behaviourTree;
    private string _wanderPointKey;

    public BTreeTask_GetNextWanderingLocation(BehaviourTree inputBehaviourTree, string inputWanderPointKey)
    {
        this._behaviourTree = inputBehaviourTree;
        this._wanderPointKey = inputWanderPointKey;
        _wanderBehaviour = inputBehaviourTree.GetComponent<WanderBehaviour>();
    }

    protected override NodeResult Execute()
    {
        if (_wanderBehaviour != null)
        {
            if (_wanderBehaviour.GetNextWanderingLocation(out Vector3 point))
            {
                _behaviourTree.Blackboard.AddData(_wanderPointKey, point);
                return NodeResult.Success;
            }
        }

        return NodeResult.Failure;
    }
}
