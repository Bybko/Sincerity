using UnityEngine;

public class StorageAction : ICharacterAction
{
    private MemoryObject _connectedObject;
    private PhysicalStatus _status;
    private Brain _brain;
    private Memory _memory;
    private GameObject _character;


    public StorageAction(GameObject character)
    {
        _character = character;
        _status = character.GetComponent<PhysicalStatus>();
        _brain = character.GetComponentInChildren<Brain>();
        _memory = character.GetComponentInChildren<Memory>();
    }


    public void Action()
    {
        if (_status.GetCurrentForeignObject() == _connectedObject.GetObjectImage()
            && _status.GetCurrentForeignObject().IsOwned())
        {
            if (_memory.FindOwnedStorageObject() != null) 
            {
                Debug.Log("Succesfully store object: " + _connectedObject.GetObjectImage().GetFoodValue());
                _character.GetComponent<CharacterAgents>().SetActionReward(0.1f);

                _memory.FindOwnedStorageObject().StoreObject(_connectedObject.GetObjectImage());
            }
        }
        else
        {
            _character.GetComponent<CharacterAgents>().SetActionReward(-0.1f);
        }

        SelfDelete();
    }


    public void SelfDelete()
    {
        _connectedObject.SetAction(null);
        _brain.OnActionRemove.Invoke();
    }


    public void ConnectWithObject(MemoryObject connectedObject) { _connectedObject = connectedObject; }
}
