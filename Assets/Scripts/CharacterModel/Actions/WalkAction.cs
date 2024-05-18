using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

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
        Debug.Log("The walk action is start!");
        if (_connectedObject.GetObjectImage() != null)
        { 
            _navMesh.SetDestination(_connectedObject.GetObjectTransform().position); 
        }
    }


    public void SelfDelete()
    {
        Debug.Log("The walk action is done!");
        _connectedObject.SetAction(null);
        _brain.OnActionRemove.Invoke();   
    }


    public void ConnectWithObject(MemoryObject connectedObject) { _connectedObject = connectedObject; }
    public void SetNavMeshAgent(NavMeshAgent agent) { _navMesh = agent; }
}
