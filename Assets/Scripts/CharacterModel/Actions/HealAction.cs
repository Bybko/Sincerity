using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAction : ICharacterAction
{
    private MemoryObject _connectedObject;
    private PhysicalStatus _status;
    private Brain _brain;


    public HealAction()
    {
        _status = GameObject.Find("Player").GetComponent<PhysicalStatus>();
        _brain = GameObject.Find("Player").GetComponentInChildren<Brain>();
    }


    public void Action()
    {
        if (_status.GetCurrentForeignObject() != null && _status.GetCurrentForeignObject().IsHealable())
        {
            _status.GetCurrentForeignObject().ChangeHP(_status.GetPotentialHeal());
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
