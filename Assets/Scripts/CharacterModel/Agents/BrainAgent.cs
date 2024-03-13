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


    public override void OnEpisodeBegin()
    {
        transform.localPosition = Vector3.zero;
        _physicalStatus.SetRandomValues();

        _receptors.CheckForeignObjects();
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Goal>(out Goal goal))
        {
            //Ну и наверн надо какое-то взаимодействие сделать?)))) 
            //Хотя это мб не здесь, мб в рецепторах это обрабатывать OnTriggerEnter, чтобы изменения о которых говорилось
            //в объекте реально произошли
            SetReward(1);
            _emotions.SetReward(1);
            _instincts.SetReward(1);
            EndEpisode();
        }

        if (other.TryGetComponent<Wall>(out Wall wall)) 
        {
            SetReward(-10f);
            _emotions.SetReward(-10f);
            _instincts.SetReward(-10f);
            EndEpisode();
        }
    }

    public float GetFinalDecision() { return _finalDecision; }
}
