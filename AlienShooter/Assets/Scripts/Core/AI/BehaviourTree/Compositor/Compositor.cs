using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compositor : BTreeNode
{
    private LinkedList<BTreeNode> children = new LinkedList<BTreeNode>();
    private LinkedListNode<BTreeNode> currentChild = null;

    public void AddChild(BTreeNode newChild)
    {
        children.AddLast(newChild);
    }
    protected override NodeResult Execute()
    {
        if (children.Count == 0)
            return NodeResult.Success;
        currentChild = children.First;
        return NodeResult.InProgress;
    }

    protected bool Next()
    {
        if (currentChild != children.Last)
        {
            currentChild = currentChild.Next;
            return true;
        }

        return false;
    }

    protected override void End()
    {
        if (currentChild == null) return;
        currentChild.Value.Abort();
        currentChild = null;
    }

    public override void SortPriority(ref int priorityRef)
    {
        base.SortPriority(ref priorityRef);
        foreach (var child in children)
        {
            child.SortPriority(ref priorityRef);
        }
    }

    protected BTreeNode GetCurrentChild() => currentChild.Value;
    public override BTreeNode Get()
    {
        if (currentChild == null)
        {
            if (children.Count != 0)
            {
                return children.First.Value.Get();
            }
            else
            {
                return this;
            }
        }

        return currentChild.Value.Get();
    }
}
