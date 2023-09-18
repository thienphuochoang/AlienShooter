using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourTree : MonoBehaviour
{
    private BTreeNode root;
    private Blackboard _blackboard = new Blackboard();

    public Blackboard Blackboard
    {
        get => _blackboard;
    }

    private void Start()
    {
        ConstructTree(out root);
    }

    private void Update()
    {
        root.UpdateNode();
    }

    protected abstract void ConstructTree(out BTreeNode rootNode);
}
