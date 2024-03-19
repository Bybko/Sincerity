using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class InstinctBrainAgent : Agent
{
    [SerializeField] private Brain _brain;

    private Feeling _feeling;
    private float _instinctDecision = 0f;


    public override void CollectObservations(VectorSensor sensor)
    {
        Debug.Log("Inputs Instincts: " + _feeling.GetFoodChange() + " ;" + _feeling.GetHealthChange() + " ; " + _feeling.GetTotalHappinessChange());
        sensor.AddObservation(_feeling.GetFoodChange());
        sensor.AddObservation(_feeling.GetHealthChange());
        sensor.AddObservation(_feeling.GetTotalHappinessChange());
    }


    public override void OnActionReceived(ActionBuffers actions)
    {
        _instinctDecision = actions.ContinuousActions[0];
        _brain.IsInstinctsDecisionReady(true);
    }


    public void SetFeeling(Feeling newFeeling) { _feeling = newFeeling;  }

    public float GetInstinctDecision() { return _instinctDecision; }    
}
