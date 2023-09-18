using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTreeTask_Wait : BTreeNode
{
    private float _waitTime;
    private float _timeElapsed = 0f;

    public BTreeTask_Wait(float waitTime)
    {
        this._waitTime = waitTime;
    }
    protected override NodeResult Execute()
    {
        if (_waitTime <= 0)
        {
            return NodeResult.Success;
        }
        Debug.Log("Wait started with duration: " + _waitTime);
        _timeElapsed = 0f;
        return NodeResult.InProgress;
    }

    protected override NodeResult Update()
    {
        _timeElapsed += Time.deltaTime;
        if (_timeElapsed >= _waitTime)
        {
            Debug.Log("Wait finished");
            return NodeResult.Success;
        }
        return NodeResult.InProgress;
    }

    protected override void End()
    {
        base.End();
    }
}
