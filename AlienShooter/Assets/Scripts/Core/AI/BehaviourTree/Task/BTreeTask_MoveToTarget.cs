using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTreeTask_MoveToTarget : BTreeNode
{
    private NavMeshAgent _navMeshAgent;
    private string _targetKey;
    private GameObject _target;
    private float _distance;
    private BehaviourTree _behaviourTree;
    private IBehaviourTree _behaviourTreeInterface;

    public BTreeTask_MoveToTarget(BehaviourTree inputBehaviourTree, string inputTargetKey, float inputDistance)
    {
        this._targetKey = inputTargetKey;
        this._behaviourTree = inputBehaviourTree;
        this._distance = inputDistance;
        _navMeshAgent = this._behaviourTree.GetComponent<NavMeshAgent>();
        _behaviourTreeInterface = _behaviourTree.GetBehaviourTreeInterface();
    }

    protected override NodeResult Execute()
    {
        Blackboard blackboard = _behaviourTree.Blackboard;
        if (blackboard == null || blackboard.GetData(_targetKey, out _target) == false)
            return NodeResult.Failure;

        if (_navMeshAgent == null)
            return NodeResult.Failure;

        if (IsTargetInDistance())
            return NodeResult.Success;

        blackboard.onBlackboardValueChange += Blackboard_OnBlackboardValueChange;
        _navMeshAgent.SetDestination(_target.transform.position);
        _navMeshAgent.speed = 2f;
        _navMeshAgent.isStopped = false;
        _behaviourTreeInterface.TriggerRunAnimation();
        return NodeResult.InProgress;
    }

    protected override NodeResult Update()
    {
        if (_target == null)
        {
            _navMeshAgent.isStopped = true;
            return NodeResult.Failure;
        }

        _navMeshAgent.SetDestination(_target.transform.position);
        if (IsTargetInDistance())
        {
            _navMeshAgent.isStopped = true;
            return NodeResult.Success;
        }

        return NodeResult.InProgress;
    }

    private void Blackboard_OnBlackboardValueChange(string key, object val)
    {
        if (key == _targetKey)
        {
            _target = (GameObject)val;
        }
    }

    private bool IsTargetInDistance()
    {
        return Vector3.Distance(_target.transform.position, _behaviourTree.transform.position) <= _distance;
    }

    protected override void End()
    {
        _navMeshAgent.isStopped = true;
        _navMeshAgent.speed = 0.5f;
        _behaviourTreeInterface.CancelRunAnimation();
        _behaviourTree.Blackboard.onBlackboardValueChange -= Blackboard_OnBlackboardValueChange;
        base.End();
    }
}
