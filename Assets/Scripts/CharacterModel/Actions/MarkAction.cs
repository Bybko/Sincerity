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
        if (_status.GetCurrentForeignObject() != null)
        {
            if (_status.GetCurrentForeignObject().GetDamageValue() < 0)
            {
                _character.GetComponentInChildren<BrainActionAgent>().SetSimpleReward(-0.2f);
            }
            else { _character.GetComponentInChildren<BrainActionAgent>().SetSimpleReward(0.2f); }

            _status.GetCurrentForeignObject().SetObjectOwner(_character.GetComponent<CharacterObject>());
            _memory.OwnObject(_connectedObject);
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
