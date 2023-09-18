using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HitSense : SenseSystem
{
    private HealthSystem _healthSystem;
    [SerializeField] private float hitMemory = 2f;
    private Dictionary<PerceptionStimuli, Coroutine> hitRecord = new Dictionary<PerceptionStimuli, Coroutine>();
    protected override bool IsStimuliSensable(PerceptionStimuli stimuli)
    {
        return hitRecord.ContainsKey(stimuli);
    }

    private void Start()
    {
        _healthSystem = GetComponent<HealthSystem>();
        _healthSystem.onDamaged += HealthSystem_onDamaged;
    }

    private void HealthSystem_onDamaged(float amountOfDamage, GameObject player)
    {
        PerceptionStimuli stimuli = player.GetComponent<PerceptionStimuli>();
        if (stimuli != null)
        {
            Coroutine newForgettingCoroutine = StartCoroutine(ForgetStimuli(stimuli));
            if (hitRecord.TryGetValue(stimuli, out Coroutine onGoingCoroutine))
            {
                StopCoroutine(onGoingCoroutine);
                hitRecord[stimuli] = onGoingCoroutine;
            }
            else
            {
                hitRecord.Add(stimuli, newForgettingCoroutine);
            }
        }
    }

    private IEnumerator ForgetStimuli(PerceptionStimuli stimuli)
    {
        yield return new WaitForSeconds(hitMemory);
        hitRecord.Remove(stimuli);
    }
}
