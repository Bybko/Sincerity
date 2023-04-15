using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class CharacterAgent : Agent
{
    [SerializeField] private EmotionalModel _emotionalModel;


    private void Start()
    {
        _emotionalModel.HungerDiedEvent += Kill;
    }

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(0f, 1f, 0f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        float moveSpeed = 15f;
        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed; 
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent<Goal>(out Goal goal))
        {
            float reward = goal.Eating();
            SetReward(reward);
            _emotionalModel.UpdateHapinnes(reward);
            EndEpisode();
        }

        if (col.TryGetComponent<Wall>(out Wall wall))
        {
            SetReward(-30f);
            EndEpisode();
        }
    }

    private void Kill()
    {
        _emotionalModel.ResetHP();
        SetReward(-40f);
        EndEpisode();
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }
}
