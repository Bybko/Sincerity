using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : ICharacterAction
{
    private MemoryObject _connectedObject;
    private PhysicalStatus _status;
    private Brain _brain;


    public AttackAction()
    {
        _status = GameObject.Find("Player").GetComponent<PhysicalStatus>();
        _brain = GameObject.Find("Player").GetComponentInChildren<Brain>();
    }


    public void Action()
    {
        if (_status.GetCurrentForeignObject() != null)
        {
            //Debug.Log("Real attack on object " + _status.GetCurrentForeignObject().GetFoodValue()  + " by damage " +
            //   _status.GetPotentialDamage());
            //Debug.Log("HP right now is: " + _status.GetCurrentForeignObject().GetObjectHP());
            _status.GetCurrentForeignObject().ChangeHP(_status.GetPotentialDamage());
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
