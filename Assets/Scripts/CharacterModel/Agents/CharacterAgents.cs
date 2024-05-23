using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAgents : MonoBehaviour
{
    [SerializeField] private EventHandler _events;
    [SerializeField] private Receptors _receptors;
    [SerializeField] private PhysicalStatus _physicalStatus;
    [SerializeField] private Subconscious _subconscious;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Brain _brain;
    [SerializeField] private Vector3 _startPosition;

    [Header("Agents")]
    [SerializeField] private BrainActionAgent _rationalAction;
    [SerializeField] private BrainAgent _rationalDecision;
    [SerializeField] private EmotionalBrainAgent _emotionalDecision;
    [SerializeField] private InstinctBrainAgent _instinctDecision;



    public void ResetAgent()
    {
        _brain.ResetMemory();
        _brain.ResetSearchStatus();

        _receptors.ResetCoroutinesQueue();

        _playerTransform.localPosition = _startPosition;
        _physicalStatus.SetRandomValues();

        _subconscious.WakeUp();
    }


    public void SetComplexReward(float reward)
    {
        _rationalAction.SetReward(reward);
        _rationalDecision.SetReward(reward);
        _emotionalDecision.SetReward(reward);
        _instinctDecision.SetReward(reward);
    }


    public void SetActionReward(float reward)
    {
        _rationalAction.SetReward(reward);
    }


    public void TotalEndEpisode()
    {
        _rationalAction.EndEpisode();
        _rationalDecision.EndEpisode();
        _emotionalDecision.EndEpisode();
        _instinctDecision.EndEpisode();
    }


    public float GetAgentFoodResources() { return _physicalStatus.GetCurrentFoodResources(); }
    public float GetAgentHealth() { return _physicalStatus.GetHealth(); }
}
