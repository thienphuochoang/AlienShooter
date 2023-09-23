using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTreeTask_Attack : BTreeNode
{
    private NavMeshAgent _navMeshAgent;
    private BehaviourTree _behaviourTree;
    private string _targetKey;
    private GameObject _target;
    private float _attackDistance = 1f;

    public BTreeTask_Attack(BehaviourTree behaviourTree, string targetKey)
    {
        _behaviourTree = behaviourTree;
        _targetKey = targetKey;
        _navMeshAgent = _behaviourTree.GetComponent<NavMeshAgent>();
    }

    protected override NodeResult Execute()
    {
        if (!_behaviourTree || _behaviourTree.Blackboard == null ||
            !_behaviourTree.Blackboard.GetData(_targetKey, out _target))
        {
            return NodeResult.Failure;
        }

        IBehaviourTree behaviourTreeInterface = _behaviourTree.GetBehaviourTreeInterface();
        if (behaviourTreeInterface == null)
            return NodeResult.Failure;
        _navMeshAgent.isStopped = true;
        behaviourTreeInterface.Attack(_target);
        return NodeResult.Success;
    }

    /*protected override NodeResult Update()
    {
        if (IsTargetInAttackDistance())
        {
            _navMeshAgent.isStopped = true;
            return NodeResult.Success;
        }

        return NodeResult.InProgress;
    }*/
    private bool IsTargetInAttackDistance()
    {
        return Vector3.Distance(_target.transform.position, _behaviourTree.transform.position) <= _attackDistance;
    }
    
}
