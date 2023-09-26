using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BTreeTask_Group : BTreeNode
{
    private BTreeNode _root;
    protected BehaviourTree _behaviourTree;

    public BTreeTask_Group(BehaviourTree tree)
    {
        _behaviourTree = tree;
    }

    protected abstract void ConstructTree(out BTreeNode root);

    protected override NodeResult Execute()
    {
        return NodeResult.InProgress;
    }

    protected override NodeResult Update()
    {
        return _root.UpdateNode();
    }

    protected override void End()
    {
        _root.Abort();
        base.End();
    }

    public override void SortPriority(ref int priorityRef)
    {
        base.SortPriority(ref priorityRef);
        _root.SortPriority(ref priorityRef);
    }

    public override void Initialize()
    {
        base.Initialize();
        ConstructTree(out _root);
    }

    public override BTreeNode Get()
    {
        return _root.Get();
    }
}
