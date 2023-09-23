using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackboardDecorator : Decorator
{
    public enum RunCondition
    {
        KeyExist,
        KeyNotExist
    }

    public enum NotifyRule
    {
        RunConditionChange,
        KeyValueChange,
    }

    public enum NotifyAbort
    {
        None,
        Self,
        Lower,
        Both
    }

    private BehaviourTree _behaviourTree;
    private string _key;
    private object _value;
    private RunCondition _runCondition;
    private NotifyRule _notifyRule;
    private NotifyAbort _notifyAbort;

    public BlackboardDecorator(BehaviourTree bahaviourTree,
        BTreeNode child,
        string key,
        RunCondition runCondition,
        NotifyRule notifyRule,
        NotifyAbort notifyAbort) : base(child)
    {
        _behaviourTree = bahaviourTree;
        _key = key;
        _runCondition = runCondition;
        _notifyRule = notifyRule;
        _notifyAbort = notifyAbort;
    }

    protected override NodeResult Execute()
    {
        Blackboard blackboard = _behaviourTree.Blackboard;
        if (blackboard == null)
            return NodeResult.Failure;
        
        blackboard.onBlackboardValueChange -= CheckNotify;
        blackboard.onBlackboardValueChange += CheckNotify;
        if (CheckRunCondition())
        {
            return NodeResult.InProgress;
        }
        else
        {
            return NodeResult.Failure;
        }
    }

    private void CheckNotify(string key, object val)
    {
        if (_key != key)
            return;
        if (_notifyRule == NotifyRule.RunConditionChange)
        {
            bool previousExist = _value != null;
            bool currentExist = val != null;
            if (previousExist != currentExist)
            {
                Notify();
            }
        }
        else if (_notifyRule == NotifyRule.KeyValueChange)
        {
            if (val != _value)
            {
                Notify();
            }
        }
    }

    private void Notify()
    {
        switch (_notifyAbort)
        {
            case NotifyAbort.None:
            {
                break;
            }
            case NotifyAbort.Self:
            {
                AbortSelf();
                break;
            }
            case NotifyAbort.Lower:
            {
                AbortLower();
                break;
            }
            case NotifyAbort.Both:
            {
                AbortBoth();
                break;
            }
        }
    }

    private void AbortSelf()
    {
        Abort();
    }

    protected override void End()
    {
        GetChild().Abort();
        base.End();
    }

    private void AbortLower()
    {
        _behaviourTree.AbortLowerThan(GetPriority());
    }

    private void AbortBoth()
    {
        Abort();
        AbortLower();
    }

    private bool CheckRunCondition()
    {
        bool isExist = _behaviourTree.Blackboard.GetData(_key, out _value);
        switch (_runCondition)
        {
            case RunCondition.KeyExist:
                return isExist;
            case RunCondition.KeyNotExist:
                return !isExist;
        }

        return false;
    }

    protected override NodeResult Update()
    {
        return GetChild().UpdateNode();
    }
}
