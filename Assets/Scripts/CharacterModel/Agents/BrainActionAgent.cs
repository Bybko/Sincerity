using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class BrainActionAgent : Agent
{
    //for train episode initialize
    [SerializeField] private Receptors _receptors;
    [SerializeField] private PhysicalStatus _physicalStatus;
    [SerializeField] private Subconscious _subconscious;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private InstinctBrainAgent _instincts;
    [SerializeField] private EmotionalBrainAgent _emotions;
    [SerializeField] private BrainAgent _brainAgent;

    [SerializeField] private Brain _brain;

    private float _instinctDecision = 0f;
    private float _emotionalDecision = 0f;
    private float _finalDecision = 0f;
    private ICharacterAction _decidedAction;


    //for a training
    private void Update()
    {
        CheckTrainEpisode();
    }


    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(_instinctDecision);
        sensor.AddObservation(_emotionalDecision);
        sensor.AddObservation(_finalDecision);
    }


    public override void OnActionReceived(ActionBuffers actions)
    {
        int decidedActionNumber = 0;
        for (int i = 0; i < actions.ContinuousActions.Length; i++) 
        {
            if (actions.ContinuousActions[decidedActionNumber] < actions.ContinuousActions[i])
            {
                decidedActionNumber = i;
            }
        }

        switch (decidedActionNumber)
        {
            case 0:
                _decidedAction = null;
                break;
            case 1:
                _decidedAction = new InteractionAction();
                break;
        }

        _brain.IsFinalActionReady(true);
    }


    public override void OnEpisodeBegin()
    {
        _brain.ResetMemory();

        _receptors.StopAllCoroutines();
        _brain.StopAllCoroutines();

        _playerTransform.localPosition = Vector3.zero;
        _physicalStatus.SetRandomValues();

        _subconscious.WakeUp();
    }


    public void CheckTrainEpisode()
    {
        if (_physicalStatus.GetHealth() <= 0)
        {
            SetReward(-100f);
            _brainAgent.SetReward(-100f);
            _emotions.SetReward(-100f);
            _instincts.SetReward(-100f);
            EndEpisode();
        }

        if (_physicalStatus.GetHealth() == 100)
        {
            SetReward(10f);
            _brainAgent.SetReward(10f);
            _emotions.SetReward(10f);
            _instincts.SetReward(10f);
        }

        if (_physicalStatus.GetCurrentFoodResources() == 100)
        {
            SetReward(5f);
            _brainAgent.SetReward(5f);
            _emotions.SetReward(5f);
            _instincts.SetReward(5f);
        }

        if (_subconscious.GetHappines() == 0)
        {
            SetReward(100f);
            _brainAgent.SetReward(100f);
            _emotions.SetReward(100f);
            _instincts.SetReward(100f);
            EndEpisode();
        }
    }


    public void SetInputs(float instinctDecision, float emotionalDecision, float finalDecision)
    {
        _instinctDecision = instinctDecision;
        _emotionalDecision = emotionalDecision;
        _finalDecision = finalDecision;
    }


    public ICharacterAction GetAction() { return _decidedAction; }
}