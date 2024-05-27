using UnityEngine;

public class MarkAction : ICharacterAction
{
    private MemoryObject _connectedObject;
    private PhysicalStatus _status;
    private Brain _brain;
    private Memory _memory;
    private GameObject _character;


    public MarkAction(GameObject character)
    {
        _character = character;
        _status = character.GetComponent<PhysicalStatus>();
        _brain = character.GetComponentInChildren<Brain>();
        _memory = character.GetComponentInChildren<Memory>();
    }


    public void Action()
    {
        if (_status.GetCurrentForeignObject() == _connectedObject.GetObjectImage() && 
            !_connectedObject.GetObjectImage().IsOwned())
        {
            _character.GetComponent<CharacterAgents>().SetActionReward(0.1f);
            if (_connectedObject.GetObjectImage() is StorageObject) 
            { 
                _character.GetComponent<CharacterAgents>().SetActionReward(0.4f); 
            }

            _connectedObject.GetObjectImage().SetObjectOwner(_character.GetComponent<CharacterObject>());
            _memory.OwnObject(_connectedObject);
        }
        else { _character.GetComponent<CharacterAgents>().SetActionReward(-0.1f); }

        SelfDelete();
    }


    public void SelfDelete()
    {
        _connectedObject.SetAction(null);
        _brain.OnActionRemove.Invoke();
    }


    public void ConnectWithObject(MemoryObject connectedObject) { _connectedObject = connectedObject; }
}
