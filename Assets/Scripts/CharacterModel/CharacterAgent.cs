using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class CharacterAgent : Agent
{
    [SerializeField] private Subconscious _emotionalModel;


    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(Random.Range(-5.5f, 5.5f), 1f, Random.Range(-6f, -2f));
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
            _emotionalModel.ChangeHapinnes(reward);
            EndEpisode();
        }

        if (col.TryGetComponent<Wall>(out Wall wall))
        {
            SetReward(-30f);
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
