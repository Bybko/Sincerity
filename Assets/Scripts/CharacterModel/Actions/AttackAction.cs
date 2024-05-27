using UnityEngine;
using UnityEngine.TextCore.Text;

public class AttackAction : ICharacterAction
{
    private MemoryObject _connectedObject;
    private PhysicalStatus _status;
    private Brain _brain;
    private GameObject _character;


    public AttackAction(GameObject character)
    {
        _character = character;
        _status = character.GetComponent<PhysicalStatus>();
        _brain = character.GetComponentInChildren<Brain>();
    }


    public void Action()
    {
        if (_status.GetCurrentForeignObject() == _connectedObject.GetObjectImage())
        {
            if (_connectedObject.GetObjectImage() is StorageObject) 
            { _character.GetComponent<CharacterAgents>().SetActionReward(-2f); }
            _connectedObject.GetObjectImage().ChangeHP(_status.GetPotentialDamage());
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
