using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class TrainingController : Agent
{
    [SerializeField] private EventHandler _events;
    [SerializeField] private Receptors _receptors;
    [SerializeField] private PhysicalStatus _physicalStatus;
    [SerializeField] private Subconscious _subconscious;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Brain _brain;

    [Header("Agents")]
    [SerializeField] private BrainActionAgent _rationalAction;
    [SerializeField] private BrainAgent _rationalDecision;
    [SerializeField] private EmotionalBrainAgent _emotionsDecision;
    [SerializeField] private InstinctBrainAgent _instinctDecision;


/*    private void Update()
    {
        CheckTrainEpisode();
    }


    public void CheckTrainEpisode()
    {
        if (_physicalStatus.GetHealth() <= 0)
        {
            _rationalAction.SetReward(-100f);
            _rationalDecision.SetReward(-100f);
            _emotionsDecision.SetReward(-100f);
            _instinctDecision.SetReward(-100f);
            EndEpisode();
        }

        if (_physicalStatus.GetHealth() == 100)
        {
            _rationalAction.SetReward(50f);
            _rationalDecision.SetReward(50f);
            _emotionsDecision.SetReward(50f);
            _instinctDecision.SetReward(50f);
            EndEpisode();
        }

        if (_physicalStatus.GetCurrentFoodResources() == 100)
        {
            _rationalAction.SetReward(25f);
            _rationalDecision.SetReward(25f);
            _emotionsDecision.SetReward(25f);
            _instinctDecision.SetReward(25f);
            EndEpisode();
        }
    }


    public override void OnEpisodeBegin()
    {
        _events.OnEpisodeReset.Invoke();

        _brain.ResetMemory();
        _brain.ResetSearchStatus();

        _receptors.ResetCoroutinesQueue();

        _playerTransform.localPosition = Vector3.zero;
        _physicalStatus.SetRandomValues();

        _subconscious.WakeUp();
    }*/
}
