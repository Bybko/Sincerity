using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class BrainAgent : Agent
{
    [SerializeField] private Brain _brain;

    private float _instinctDecision = 0f;
    private float _emotionalDecision = 0f;
    private float _finalDecision = 0f;

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(_instinctDecision);
        sensor.AddObservation(_emotionalDecision);
    }


    public override void OnActionReceived(ActionBuffers actions)
    {
        _finalDecision = actions.ContinuousActions[0];
        _brain.IsFinalDecisionReady(true);
    }


    public void SetInputs(float instinctDecision, float emotionalDecision)
    {
        _instinctDecision = instinctDecision;
        _emotionalDecision = emotionalDecision;
    } 


    public float GetFinalDecision() { return _finalDecision; }
}
