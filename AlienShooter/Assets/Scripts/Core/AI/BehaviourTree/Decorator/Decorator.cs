using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Decorator : BTreeNode
{
    private BTreeNode child;

    protected BTreeNode GetChild() => child;
    public Decorator(BTreeNode inputChild)
    {
        child = inputChild;
    }

    public override void SortPriority(ref int priorityRef)
    {
        base.SortPriority(ref priorityRef);
        child.SortPriority(ref priorityRef);
    }

    public override BTreeNode Get()
    {
        return child.Get();
    }

    public override void Initialize()
    {
        base.Initialize();
        child.Initialize();
    }
}
