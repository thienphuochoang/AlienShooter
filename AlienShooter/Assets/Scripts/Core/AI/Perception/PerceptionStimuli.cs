using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerceptionStimuli : MonoBehaviour
{
    private void Start()
    {
        SenseSystem.RegisterStimuli(this);
    }

    private void OnDestroy()
    {
        SenseSystem.UnRegisterStimuli(this);
    }
}
