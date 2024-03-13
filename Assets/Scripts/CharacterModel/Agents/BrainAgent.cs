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
        sensor.AddObservation(_instincts.GetInstinctDecision());
        sensor.AddObservation(_emotions.GetEmotionalDecision());
    }


    public override void OnActionReceived(ActionBuffers actions)
    {
        _finalDecision = actions.ContinuousActions[0];
    }


    public override void OnEpisodeBegin()
    {
        _playerTransform.localPosition = Vector3.zero;
        _physicalStatus.SetRandomValues();

        _receptors.CheckForeignObjects();
    }


    public void CheckTrainEpisode()
    {
        //Ну и наверн надо какое-то взаимодействие сделать?)))) 
        //Хотя это мб не здесь, мб в рецепторах это обрабатывать OnTriggerEnter, чтобы изменения о которых говорилось
        //в объекте реально произошли
        if (_physicalStatus.GetHealth() <= 0)
        {
            SetReward(-100f);
            Debug.Log("Ti eblan");
            EndEpisode();
        }

        if (_physicalStatus.GetHealth() == 100)
        {
            Debug.Log("Zdorovi");
            SetReward(10f);
        }

        if (_physicalStatus.GetCurrentFoodResources() == 100)
        {
            Debug.Log("Sity");
            SetReward(5f);
        }

        if (_subconscious.GetHappines() == 0) 
        {
            SetReward(100f);
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
