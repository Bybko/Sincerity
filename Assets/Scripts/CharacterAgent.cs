using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class CharacterAgent : Agent
{
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private Material _winMaterial;
    [SerializeField] private Material _loseMaterial;
    [SerializeField] private MeshRenderer _floorMeshRenderer;

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(Random.Range(-6f, +6f), 1f, Random.Range(-6f, +1f));
        _targetTransform.localPosition = new Vector3(Random.Range(-5f, +5f), 1f, Random.Range(+2f, +5f));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(_targetTransform.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        float moveSpeed = 5f;
        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed; 
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent<Goal>(out Goal goal))
        {
            SetReward(+1f);
            _floorMeshRenderer.material = _winMaterial;
            EndEpisode();
        }

        if (col.TryGetComponent<Wall>(out Wall wall))
        {
            SetReward(-1f);
            _floorMeshRenderer.material = _loseMaterial;
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
