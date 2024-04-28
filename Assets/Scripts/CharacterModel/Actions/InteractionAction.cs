using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class InteractionAction : ICharacterAction
{
    public bool isActionFinished = false;

    private MemoryObject _connectedObject;
    private NavMeshAgent _navMesh;
    private Brain _brain;


    public InteractionAction()
    {
        _navMesh = GameObject.Find("Player").GetComponent<NavMeshAgent>();
        _brain = GameObject.Find("Player").GetComponentInChildren<Brain>();
    }


    public void Action()
    {
        MoveTo();
    }


    private void MoveTo()
    {
        _navMesh.SetDestination(_connectedObject.GetObjectTransform().position);
    }


    public void SelfDelete()
    {
        _connectedObject.SetAction(null);
        _brain.OnActionRemove.Invoke();   
    }


    public void ConnectWithObject(MemoryObject connectedObject) { _connectedObject = connectedObject; }
    public void SetNavMeshAgent(NavMeshAgent agent) { _navMesh = agent; }
}
