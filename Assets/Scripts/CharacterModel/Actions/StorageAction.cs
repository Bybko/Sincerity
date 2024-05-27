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
            if (_memory.FindFreeOwnedStorageObject() != null && !(_connectedObject.GetObjectImage() is StorageObject)) 
            {
                Debug.Log("Succesfully store object: " + _connectedObject.GetObjectImage().GetFoodValue());
                _character.GetComponent<CharacterAgents>().SetActionReward(0.3f);

                _memory.FindFreeOwnedStorageObject().StoreObject(_connectedObject.GetObjectImage());
                _connectedObject.GetObjectImage().SetStoredStatus(true);
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
