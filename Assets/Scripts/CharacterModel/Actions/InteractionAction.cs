using UnityEngine;

public class InteractionAction : ICharacterAction
{
    private MemoryObject _connectedObject;
    private PhysicalStatus _status;
    private Brain _brain;
    private GameObject _character;


    public InteractionAction(GameObject character)
    {
        _character = character;
        _status = character.GetComponent<PhysicalStatus>();
        _brain = character.GetComponentInChildren<Brain>();
    }


    public void Action()
    {
        ForeignObject objectImage = _connectedObject.GetObjectImage();
        if (_status.GetCurrentForeignObject() == objectImage && _status.GetCurrentForeignObject().IsOwned())
        {
            _character.GetComponent<CharacterAgents>().SetActionReward(0.1f);

            if (objectImage is StorageObject) 
            {
                StorageObject objectStore = (StorageObject)objectImage;
                objectStore.SetVisitor(_character.GetComponent<CharacterObject>());
                objectStore.Interact();
            }
            else { objectImage.Interact(); }
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
