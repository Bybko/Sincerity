using UnityEngine;
using UnityEngine.AI;

public class InteractionAction : ICharacterAction
{
    public bool isActionFinished = false;

    private ForeignObject _connectedObject;
    private NavMeshAgent _navMesh;


    public InteractionAction()
    {
        _navMesh = GameObject.Find("Player").GetComponent<NavMeshAgent>();
    }


    public void Action()
    {
        MoveTo();
    }


    private void MoveTo()
    {
        _navMesh.SetDestination(_connectedObject.gameObject.transform.position);
    }


    public void ConnectWithObject(ForeignObject connectedObject) { _connectedObject = connectedObject; }
    public void SetNavMeshAgent(NavMeshAgent agent) { _navMesh = agent; }
}
