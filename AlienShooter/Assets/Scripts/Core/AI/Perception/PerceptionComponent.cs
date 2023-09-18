using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerceptionComponent : MonoBehaviour
{
    [SerializeField]
    private SenseSystem[] senses;
    private LinkedList<PerceptionStimuli> currentPerceivedStimulis = new LinkedList<PerceptionStimuli>();
    private PerceptionStimuli targetStimuli;
    public delegate void OnPerceptionTargetChanged(GameObject target, bool sensed);
    public event OnPerceptionTargetChanged onPerceptionTargetChanged;
    private void Start()
    {
        foreach (SenseSystem sense in senses)
        {
            sense.onPerceptionUpdated += Sense_Updated;
        }
    }

    private void Sense_Updated(PerceptionStimuli stimuli, bool successfulSensed)
    {
        var nodeFound = currentPerceivedStimulis.Find(stimuli);
        if (successfulSensed)
        {
            if (nodeFound != null)
            {
                currentPerceivedStimulis.AddAfter(nodeFound, stimuli);
            }
            else
            {
                currentPerceivedStimulis.AddLast(stimuli);
            }
        }
        else
        {
            currentPerceivedStimulis.Remove(nodeFound);
        }

        if (currentPerceivedStimulis.Count != 0)
        {
            PerceptionStimuli highestStimuli = currentPerceivedStimulis.First.Value;
            if (targetStimuli == null || targetStimuli != highestStimuli)
            {
                targetStimuli = highestStimuli;
                onPerceptionTargetChanged?.Invoke(targetStimuli.gameObject, true);
            }
        }
        else
        {
            if (targetStimuli != null)
            {
                onPerceptionTargetChanged?.Invoke(targetStimuli.gameObject, false);
                targetStimuli = null;
            }
        }
    }
}
