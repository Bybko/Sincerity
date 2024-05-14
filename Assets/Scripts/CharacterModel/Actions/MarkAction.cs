using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MarkAction : ICharacterAction
{
    private MemoryObject _connectedObject;
    private PhysicalStatus _status;
    private Brain _brain;


    public MarkAction()
    {
        _status = GameObject.Find("Player").GetComponent<PhysicalStatus>();
        _brain = GameObject.Find("Player").GetComponentInChildren<Brain>();
    }


    public void Action()
    {
        if (_status.GetCurrentForeignObject() != null)
        {
            _status.GetCurrentForeignObject().SetObjectOwner(true);
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
