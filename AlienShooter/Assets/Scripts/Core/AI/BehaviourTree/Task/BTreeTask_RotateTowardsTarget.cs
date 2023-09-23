using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTreeTask_RotateTowardsTarget : BTreeNode
{
    private BehaviourTree _behaviourTree;
    private string _targetKey;
    private float _acceptableDegrees;
    private GameObject _target;
    private IBehaviourTree _behaviourTreeInterface;

    public BTreeTask_RotateTowardsTarget(BehaviourTree behaviourTree, 
        string targetKey,
        float acceptableDegrees)
    {
        _behaviourTree = behaviourTree;
        _targetKey = targetKey;
        _acceptableDegrees = acceptableDegrees;
        _behaviourTreeInterface = _behaviourTree.GetBehaviourTreeInterface();
    }

    protected override NodeResult Execute()
    {
        if (_behaviourTree == null || _behaviourTree.Blackboard == null)
            return NodeResult.Failure;

        if (_behaviourTreeInterface == null)
            return NodeResult.Failure;
        
        if (!_behaviourTree.Blackboard.GetData(_targetKey, out _target))
            return NodeResult.Failure;

        if (IsInAcceptableDegrees())
            return NodeResult.Success;

        _behaviourTree.Blackboard.onBlackboardValueChange += BlackboardValueChanged;

        return NodeResult.InProgress;
    }

    private void BlackboardValueChanged(string key, object val)
    {
        if (key == _targetKey)
        {
            _target = (GameObject)val;
        }
    }

    protected override NodeResult Update()
    {
        if (_target == null)
            return NodeResult.Failure;
        
        if (IsInAcceptableDegrees())
            return NodeResult.Success;
        
        _behaviourTreeInterface.RotateTowards(_target);
        return NodeResult.InProgress;
    }

    protected override void End()
    {
        _behaviourTree.Blackboard.onBlackboardValueChange -= BlackboardValueChanged;
        base.End();
    }

    private bool IsInAcceptableDegrees()
    {
        if (_target == null) return false;
        Vector3 targetDirection = (_target.transform.position - _behaviourTree.transform.position).normalized;
        Vector3 direction = _behaviourTree.transform.forward;
        float degrees = Vector3.Angle(targetDirection, direction);
        return degrees <= _acceptableDegrees;
    }
}
