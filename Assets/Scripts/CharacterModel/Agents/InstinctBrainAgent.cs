using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class InstinctBrainAgent : Agent
{
    private Feeling _feeling;
    private float _instinctDecision = 0f;


    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(_feeling.GetFoodChange());
        sensor.AddObservation(_feeling.GetHealthChange());
        sensor.AddObservation(_feeling.GetTotalHappinessChange());
    }


    public override void OnActionReceived(ActionBuffers actions)
    {
        _instinctDecision = actions.ContinuousActions[0];
    }


    public void SetFeeling(Feeling newFeeling) { _feeling = newFeeling;  }

    public float GetInstinctDecision() { return _instinctDecision; }    
}
