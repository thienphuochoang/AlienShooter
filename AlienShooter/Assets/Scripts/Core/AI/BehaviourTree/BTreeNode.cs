using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeResult
{
    Failure,
    Success,
    InProgress
}

public abstract class BTreeNode
{
    private bool hasStarted = false;
    private int priority;

    public virtual void SortPriority(ref int priorityRef)
    {
        priority = priorityRef++;
        Debug.Log(this + " has priority: " + priority);
    }

    public int GetPriority() => priority;

    public virtual BTreeNode Get()
    {
        return this;
    }

    public NodeResult UpdateNode()
    {
        if (hasStarted == false)
        {
            hasStarted = true;
            NodeResult executeNodeResult = Execute();
            if (executeNodeResult != NodeResult.InProgress)
            {
                EndNode();
                return executeNodeResult;
            }
        }

        NodeResult updateResult = Update();
        if (updateResult != NodeResult.InProgress)
        {
            EndNode();
        }

        return updateResult;
    }

    protected virtual NodeResult Update()
    {
        return NodeResult.Success;
    }
    private void EndNode()
    {
        hasStarted = false;
        End();
    }

    protected virtual void End()
    {
        
    }

    public void Abort()
    {
        EndNode();
    }
    protected virtual NodeResult Execute()
    {
        return NodeResult.Success;
    }

    public virtual void Initialize()
    {
        
    }
}
