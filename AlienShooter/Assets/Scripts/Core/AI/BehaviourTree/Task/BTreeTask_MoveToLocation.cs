using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTreeTask_MoveToLocation : BTreeNode
{
    private NavMeshAgent _navMeshAgent;
    private string _locationKey;
    private Vector3 _location;
    private float _distance;
    private BehaviourTree _behaviourTree;
    private IBehaviourTree behaviourTreeInterface;
    
    public BTreeTask_MoveToLocation(BehaviourTree inputBehaviourTree, string inputLocationKey, float inputDistance)
    {
        this._locationKey = inputLocationKey;
        this._behaviourTree = inputBehaviourTree;
        this._distance = inputDistance;
        _navMeshAgent = this._behaviourTree.GetComponent<NavMeshAgent>();
        behaviourTreeInterface = _behaviourTree.GetBehaviourTreeInterface();
    }
    
    protected override NodeResult Execute()
    {
        Blackboard blackboard = _behaviourTree.Blackboard;
        if (blackboard == null || blackboard.GetData(_locationKey, out _location) == false)
            return NodeResult.Failure;

        if (_navMeshAgent == null)
            return NodeResult.Failure;

        if (IsLocationInDistance())
            return NodeResult.Success;
        
        _navMeshAgent.SetDestination(_location);
        _navMeshAgent.isStopped = false;
        behaviourTreeInterface.TriggerWalkAnimation();
        return NodeResult.InProgress;
    }

    protected override NodeResult Update()
    {
        if (IsLocationInDistance())
        {
            _navMeshAgent.isStopped = true;
            return NodeResult.Success;
        }

        return NodeResult.InProgress;
    }

    private bool IsLocationInDistance()
    {
        return Vector3.Distance(_location, _behaviourTree.transform.position) <= _distance;
    }
    protected override void End()
    {
        _navMeshAgent.isStopped = true;
        behaviourTreeInterface.CancelWalkAnimation();
        base.End();
    }
}
