using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.AI;

public class EscapeAction : ICharacterAction
{
    public bool isActionFinished = false;

    private MemoryObject _connectedObject;
    private NavMeshAgent _navMesh;
    private GameObject _character;


    public EscapeAction(GameObject character)
    {
        _character = character;
        _navMesh = character.GetComponent<NavMeshAgent>();
    }


    public void Action()
    {

    }


    private void RanAway()
    {

    }


    public void SelfDelete()
    {

    }


    public void ConnectWithObject(MemoryObject connectedObject) { _connectedObject = connectedObject; }
    public void SetNavMeshAgent(NavMeshAgent agent) { _navMesh = agent; }
}
