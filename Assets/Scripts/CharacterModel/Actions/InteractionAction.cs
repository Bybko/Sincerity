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
        if (_status.GetCurrentForeignObject() == _connectedObject.GetObjectImage() 
            && _status.GetCurrentForeignObject().IsOwned())
        {
            Debug.Log("Succesfully interact with object: " + _connectedObject.GetObjectImage().GetFoodValue());
            _character.GetComponent<CharacterAgents>().SetActionReward(0.1f);

            _connectedObject.GetObjectImage().Interact();
        }
        else {
            _character.GetComponent<CharacterAgents>().SetActionReward(-0.1f); }

        SelfDelete();
    }


    public void SelfDelete()
    {
        _connectedObject.SetAction(null);
        _brain.OnActionRemove.Invoke();
    }


    public void ConnectWithObject(MemoryObject connectedObject) { _connectedObject = connectedObject; }
}
