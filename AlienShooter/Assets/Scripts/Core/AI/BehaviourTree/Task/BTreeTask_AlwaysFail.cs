using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTreeTask_AlwaysFail : BTreeNode
{
    protected override NodeResult Execute()
    {
        Debug.Log("Always fail");
        return NodeResult.Failure;
    }
}
