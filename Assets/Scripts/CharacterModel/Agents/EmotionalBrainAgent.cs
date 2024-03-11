using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class EmotionalBrainAgent : Agent
{
    private Feeling _feeling;
    private float _emotionalDecision = 0f;


    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(_feeling.GetMostNeedSatisfaction());
        sensor.AddObservation(_feeling.GetTotalHappinessChange());
    }


    public override void OnActionReceived(ActionBuffers actions)
    {
        _emotionalDecision = actions.ContinuousActions[0];
    }


    public void SetFeeling(Feeling newFeeling) {  _feeling = newFeeling; } 
    
    public float GetEmotionalDecision() { return _emotionalDecision; }
}
