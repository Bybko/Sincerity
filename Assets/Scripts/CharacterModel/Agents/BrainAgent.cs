using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine.AI;

public class BrainAgent : Agent
{
    [SerializeField] private InstinctBrainAgent _instincts;
    [SerializeField] private EmotionalBrainAgent _emotions;

    private float _finalDecision = 0f;


    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(_instincts.GetInstinctDecision());
        sensor.AddObservation(_emotions.GetEmotionalDecision());
    }


    public override void OnActionReceived(ActionBuffers actions)
    {
        _finalDecision = actions.ContinuousActions[0];
    }


    public float GetFinalDecision() { return _finalDecision; }
}
