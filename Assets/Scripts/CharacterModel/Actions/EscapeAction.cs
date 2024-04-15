using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EscapeAction : ICharacterAction
{
    public bool isActionFinished = false;

    private ForeignObject _connectedObject;
    private NavMeshAgent _navMesh;


    public void Action()
    {

    }


    private void RanAway()
    {

    }


    public void SetConnectedObject(ForeignObject connectedObject) { _connectedObject = connectedObject; }
    public void SetNavMeshAgent(NavMeshAgent agent) { _navMesh = agent; }
}
