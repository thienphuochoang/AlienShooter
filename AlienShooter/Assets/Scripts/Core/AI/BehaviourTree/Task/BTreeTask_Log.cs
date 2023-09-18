using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTreeTask_Log : BTreeNode
{
    private string message;

    public BTreeTask_Log(string inputMessage)
    {
        message = inputMessage;
    }

    protected override NodeResult Execute()
    {
        Debug.Log(message);
        return NodeResult.Success;
    }
}
