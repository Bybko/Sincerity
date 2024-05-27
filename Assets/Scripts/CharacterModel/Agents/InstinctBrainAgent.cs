using System;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class InstinctBrainAgent : Agent
{
    [SerializeField] private Brain _brain;
    [SerializeField] private CharacterObject _character;

    private Feeling _feeling = null;
    private float _instinctDecision = 0f;


    public override void CollectObservations(VectorSensor sensor)
    {
        float ownerDamage = 0f;
        if (_feeling.GetFeelableObject().GetOwner() != null)
        {
            CharacterObject owner = (CharacterObject)_feeling.GetFeelableObject().GetOwner();
            if (owner != _character)
            {
                ownerDamage = Math.Abs(_feeling.GetFeelableObject().GetOwner().GetDamageValue());
            }
        }


        sensor.AddObservation(_feeling.GetDanger());
        sensor.AddObservation(_feeling.GetFoodChange());
        sensor.AddObservation(_feeling.GetHealthChange());
        sensor.AddObservation(_feeling.GetCurrentFoodResources());
        sensor.AddObservation(_feeling.GetCurrentHealth());
        sensor.AddObservation(_feeling.GetTotalHappinessChange());
        sensor.AddObservation(ownerDamage);
    }


    public override void OnActionReceived(ActionBuffers actions)
    {
        _instinctDecision = actions.ContinuousActions[0];
        _brain.IsInstinctsDecisionReady(true);
    }


    public void SetFeeling(Feeling newFeeling) { _feeling = newFeeling;  }

    public float GetInstinctDecision() { return _instinctDecision; }    
}
