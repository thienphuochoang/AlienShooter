using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SenseSystem : MonoBehaviour
{
    private static List<PerceptionStimuli> _registeredStimuliList = new List<PerceptionStimuli>();
    private List<PerceptionStimuli> _perceivableStimuliList = new List<PerceptionStimuli>();

    private Dictionary<PerceptionStimuli, Coroutine>
        _forgettingRoutines = new Dictionary<PerceptionStimuli, Coroutine>();

    public delegate void OnPerceptionUpdated(PerceptionStimuli stimuli, bool successfulSensed);

    public event OnPerceptionUpdated onPerceptionUpdated;
    [SerializeField] private float forgettingTime = 3f;
    public static void RegisterStimuli(PerceptionStimuli stimuli)
    {
        if (_registeredStimuliList.Contains(stimuli))
            return;
        _registeredStimuliList.Add(stimuli);
    }

    public static void UnRegisterStimuli(PerceptionStimuli stimuli)
    {
        _registeredStimuliList.Remove(stimuli);
    }

    protected abstract bool IsStimuliSensable(PerceptionStimuli stimuli);

    private void Update()
    {
        foreach (PerceptionStimuli stimuli in _registeredStimuliList)
        {
            if (IsStimuliSensable(stimuli))
            {
                if (!_perceivableStimuliList.Contains(stimuli))
                {
                    _perceivableStimuliList.Add(stimuli);
                    if (_forgettingRoutines.TryGetValue(stimuli, out Coroutine routine))
                    {
                        StopCoroutine(routine);
                        _forgettingRoutines.Remove(stimuli);
                    }
                    else
                    {
                        onPerceptionUpdated?.Invoke(stimuli, true);
                        Debug.Log("I just sensed " + stimuli.gameObject);
                    }
                }
            }
            else
            {
                if (_perceivableStimuliList.Contains(stimuli))
                {
                    _perceivableStimuliList.Remove(stimuli);
                    _forgettingRoutines.Add(stimuli, StartCoroutine(ForgetStimuli(stimuli)));
                }
            }
        }
    }

    IEnumerator ForgetStimuli(PerceptionStimuli stimuli)
    {
        yield return new WaitForSeconds(forgettingTime);
        _forgettingRoutines.Remove(stimuli);
        onPerceptionUpdated?.Invoke(stimuli, false);
        Debug.Log("I just lost track of " + stimuli.gameObject);
    }

    protected virtual void DrawDebug()
    {
        
    }

    private void OnDrawGizmos()
    {
        DrawDebug();
    }
}
