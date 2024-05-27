using UnityEngine;
using System;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class BrainAgent : Agent
{
    [SerializeField] private Brain _brain;
    [SerializeField] private CharacterObject _character;

    private Feeling _feeling = null;
    private float _instinctDecision = 0f;
    private float _emotionalDecision = 0f;
    private float _finalDecision = 0f;

    public override void CollectObservations(VectorSensor sensor)
    {
        float isFreeStorageObject = 0f;
        if (_feeling.GetFeelableObject() is StorageObject && !_feeling.GetFeelableObject().IsOwned()) 
        { 
            isFreeStorageObject = 1f; 
        }

        float ownerDamage = 0f;
        if (_feeling.GetFeelableObject().GetOwner() != null)
        {
            CharacterObject owner = (CharacterObject)_feeling.GetFeelableObject().GetOwner();
            if (owner != _character)
            {
                ownerDamage = Math.Abs(_feeling.GetFeelableObject().GetOwner().GetDamageValue());
            }
        }


        sensor.AddObservation(_instinctDecision);
        sensor.AddObservation(_emotionalDecision);
        sensor.AddObservation(isFreeStorageObject);
        sensor.AddObservation(ownerDamage);
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

    public void SetFeeling(Feeling newFeeling) { _feeling = newFeeling; }

    public float GetFinalDecision() { return _finalDecision; }
}
