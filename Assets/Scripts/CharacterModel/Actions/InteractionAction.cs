using UnityEngine;

public class InteractionAction : ICharacterAction
{
    private MemoryObject _connectedObject;
    private PhysicalStatus _status;
    private Brain _brain;
    private Memory _memory;
    private GameObject _character;


    public InteractionAction(GameObject character)
    {
        _character = character;
        _status = character.GetComponent<PhysicalStatus>();
        _brain = character.GetComponentInChildren<Brain>();
        _memory = character.GetComponentInChildren<Memory>();
    }


    public void Action()
    {
        if (_status.GetCurrentForeignObject() != null && _status.GetCurrentForeignObject().IsOwned())
        {
            Debug.Log("Succesfully interact with object: " + _status.GetCurrentForeignObject().GetFoodValue());
            _character.GetComponentInChildren<BrainActionAgent>().SetSimpleReward(0.5f);

            //it's only for food, make it more abstract, mb by interact methods in foreignObject class
            _character.GetComponent<Receptors>().ForeignObjectLegacy(_status.GetCurrentForeignObject());
            _status.GetCurrentForeignObject().SelfDestroy();
            _memory.ReleaseObject(_connectedObject);
        }
        else { _character.GetComponentInChildren<BrainActionAgent>().SetSimpleReward(-0.5f); }

        SelfDelete();
    }


    public void SelfDelete()
    {
        _connectedObject.SetAction(null);
        _brain.OnActionRemove.Invoke();
    }


    public void ConnectWithObject(MemoryObject connectedObject) { _connectedObject = connectedObject; }
}
