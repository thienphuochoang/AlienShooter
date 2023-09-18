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
        currentChild = null;
    }

    protected BTreeNode GetCurrentChild() => currentChild.Value;
}
