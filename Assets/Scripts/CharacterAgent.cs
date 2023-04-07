using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class CharacterAgent : Agent
{
    [SerializeField] private Transform _targetTransform;


    public override void OnEpisodeBegin()
    {
        transform.position = Vector3.zero;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(_targetTransform.position);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        float moveSpeed = 5f;
        transform.position += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed; 
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent<Goal>(out Goal goal))
        {
            SetReward(+1f);
            EndEpisode();
        }

        if (col.TryGetComponent<Wall>(out Wall wall))
        {
            SetReward(-1f);
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }
}
