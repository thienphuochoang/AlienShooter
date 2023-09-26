using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourTree : MonoBehaviour
{
    private BTreeNode root;
    private Blackboard _blackboard = new Blackboard();
    private IBehaviourTree _behaviourTreeInterface;

    public Blackboard Blackboard
    {
        get => _blackboard;
    }

    private void Start()
    {
        _behaviourTreeInterface = GetComponent<IBehaviourTree>();
        ConstructTree(out root);
        SortTree();
    }

    private void Update()
    {
        root.UpdateNode();
    }

    protected abstract void ConstructTree(out BTreeNode rootNode);

    private void SortTree()
    {
        int priorityCounter = 0;
        root.Initialize();
        root.SortPriority(ref priorityCounter);
    }

    public void AbortLowerThan(int priority)
    {
        BTreeNode currentNode = root.Get();
        if (currentNode.GetPriority() > priority)
        {
            root.Abort();
        }
    }

    public IBehaviourTree GetBehaviourTreeInterface() => _behaviourTreeInterface;
}
