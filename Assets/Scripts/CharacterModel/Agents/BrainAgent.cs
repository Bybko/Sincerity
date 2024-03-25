using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class BrainAgent : Agent
{
    //for train episode initialize
    [SerializeField] private Receptors _receptors;
    [SerializeField] private PhysicalStatus _physicalStatus;
    [SerializeField] private Subconscious _subconscious;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private InstinctBrainAgent _instincts;
    [SerializeField] private EmotionalBrainAgent _emotions;

    [SerializeField] private Brain _brain;

    private float _instinctDecision = 0f;
    private float _emotionalDecision = 0f;
    private float _finalDecision = 0f;

    //for a training
    private void Update()
    {
        CheckTrainEpisode();
    }

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


    public override void OnEpisodeBegin()
    {
        _playerTransform.localPosition = Vector3.zero;
        _physicalStatus.SetRandomValues();

        _brain.ResetMemory();
    }


    public void CheckTrainEpisode()
    {
        if (_physicalStatus.GetHealth() <= 0)
        {
            SetReward(-100f);
            _emotions.SetReward(-100f);
            _instincts.SetReward(-100f);
            EndEpisode();
        }

        if (_physicalStatus.GetHealth() == 100)
        {
            SetReward(10f);
            _emotions.SetReward(10f);
            _instincts.SetReward(10f);
        }

        if (_physicalStatus.GetCurrentFoodResources() == 100)
        {
            SetReward(5f);
            _emotions.SetReward(5f);
            _instincts.SetReward(5f);
        }

        if (_subconscious.GetHappines() == 0) 
        {
            SetReward(100f);
            _emotions.SetReward(100f);
            _instincts.SetReward(100f);
            EndEpisode();
        }
    }


    public void SetInputs(float instinctDecision, float emotionalDecision)
    {
        _instinctDecision = instinctDecision;
        _emotionalDecision = emotionalDecision;
    } 


    public float GetFinalDecision() { return _finalDecision; }
}
