using UnityEngine;
using UnityEngine.AI;

public class WalkAction : ICharacterAction
{
    public bool isActionFinished = false;

    private MemoryObject _connectedObject;
    private NavMeshAgent _navMesh;
    private Brain _brain;


    public WalkAction(GameObject character)
    {
        _navMesh = character.GetComponent<NavMeshAgent>();
        _brain = character.GetComponentInChildren<Brain>();
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
