using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class WalkAction : ICharacterAction
{
    public bool isActionFinished = false;

    private MemoryObject _connectedObject;
    private NavMeshAgent _navMesh;
    private Brain _brain;
    private GameObject _character;


    public WalkAction(GameObject character)
    {
        _character = character;
        _navMesh = character.GetComponent<NavMeshAgent>();
        _brain = character.GetComponentInChildren<Brain>();
    }


    public void Action()
    {
        if (_connectedObject.GetObjectImage() != null)
        {
            if (!_connectedObject.GetObjectImage().IsStored() || 
                (_connectedObject.GetObjectImage().IsStored() && _character.GetComponent<CharacterObject>().IsInside()))
            {
                Vector3 setPosition = new Vector3(_connectedObject.GetObjectPosition().x, 
                    _character.transform.position.y, _connectedObject.GetObjectPosition().z);
                _navMesh.SetDestination(setPosition);
            }
            else { _character.GetComponent<CharacterAgents>().SetActionReward(-0.1f); }
        }
    }


    public void SelfDelete()
    {
        _connectedObject.SetAction(null);
        _brain.OnActionRemove.Invoke();   
    }


    public void ConnectWithObject(MemoryObject connectedObject) { _connectedObject = connectedObject; }
    public void SetNavMeshAgent(NavMeshAgent agent) { _navMesh = agent; }
}
