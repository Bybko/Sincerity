using UnityEngine;

public class MarkAction : ICharacterAction
{
    private MemoryObject _connectedObject;
    private PhysicalStatus _status;
    private Brain _brain;
    private GameObject _character;


    public MarkAction(GameObject character)
    {
        _character = character;
        _status = character.GetComponent<PhysicalStatus>();
        _brain = character.GetComponentInChildren<Brain>();
    }


    public void Action()
    {
        if (_status.GetCurrentForeignObject() != null)
        {
            _status.GetCurrentForeignObject().SetObjectOwner(_character.GetComponent<CharacterObject>());
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
