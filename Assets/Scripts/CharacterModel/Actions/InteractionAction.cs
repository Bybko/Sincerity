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
        if (_status.GetCurrentForeignObject() != null && _status.GetCurrentForeignObject().IsOwned())
        {
            //it's only for food, make it more abstract, mb by interact methods in foreignObject class
            _character.GetComponent<Receptors>().ForeignObjectLegacy(_status.GetCurrentForeignObject());
            _status.GetCurrentForeignObject().SelfDestroy();
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
