using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine.AI;

public class BrainAgent : Agent
{
    //temporary for train episode initialize
    [SerializeField] private Receptors _receptors;
    [SerializeField] private PhysicalStatus _physicalStatus;
    [SerializeField] private Subconscious _subconscious;
    [SerializeField] private Transform _playerTransform;

    [SerializeField] private Brain _brain;
    [SerializeField] private InstinctBrainAgent _instincts;
    [SerializeField] private EmotionalBrainAgent _emotions;

    private float _finalDecision = 0f;

    //Only for a training
    private void Update()
    {
        CheckTrainEpisode();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        //Debug.Log("Feelings: " + _emotions.GetEmotionalDecision());
        //Debug.Log("Instincts: " + _instincts.GetInstinctDecision());
        sensor.AddObservation(_instincts.GetInstinctDecision());
        sensor.AddObservation(_emotions.GetEmotionalDecision());
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

        StartCoroutine(_receptors.CheckForeignObjects());
    }


    public void CheckTrainEpisode()
    {
        //�� � ������ ���� �����-�� �������������� �������?)))) 
        //���� ��� �� �� �����, �� � ���������� ��� ������������ OnTriggerEnter, ����� ��������� � ������� ����������
        //� ������� ������� ���������
        if (_physicalStatus.GetHealth() <= 0)
        {
            SetReward(-100f);
            _emotions.SetReward(-100f);
            _instincts.SetReward(-100f);
            Debug.Log("Ti eblan");
            EndEpisode();
        }

        if (_physicalStatus.GetHealth() == 100)
        {
            Debug.Log("Zdorovi");
            SetReward(10f);
            _emotions.SetReward(10f);
            _instincts.SetReward(10f);
        }

        if (_physicalStatus.GetCurrentFoodResources() == 100)
        {
            Debug.Log("Sity");
            SetReward(5f);
            _emotions.SetReward(5f);
            _instincts.SetReward(5f);
        }

        if (_subconscious.GetHappines() == 0) 
        {
            SetReward(100f);
            _emotions.SetReward(100f);
            _instincts.SetReward(100f);
            Debug.Log("Ti bog");
            EndEpisode();
        }
        /*float award = _subconscious.GetHappines(); //maybe happines difference, not current level, think layter
        Debug.Log("Award: " + award);
        SetReward(award);
        _emotions.SetReward(award);
        _instincts.SetReward(award);
        EndEpisode();*/
    }


    public float GetFinalDecision() { return _finalDecision; }
}
