using UnityEngine;

public class HealAction : ICharacterAction
{
    private MemoryObject _connectedObject;
    private PhysicalStatus _status;
    private Brain _brain;


    public HealAction(GameObject character)
    {
        _status = character.GetComponent<PhysicalStatus>();
        _brain = character.GetComponentInChildren<Brain>();
    }


    public void Action()
    {
        Debug.Log("The heal action is start!");
        if (_status.GetCurrentForeignObject() != null)
        {
            _status.GetCurrentForeignObject().ChangeHP(_status.GetPotentialHeal());
        }

        SelfDelete();
    }


    public void SelfDelete()
    {
        Debug.Log("The heal action is done!");
        _connectedObject.SetAction(null);
        _brain.OnActionRemove.Invoke();
    }


    public void ConnectWithObject(MemoryObject connectedObject) { _connectedObject = connectedObject; }
}
