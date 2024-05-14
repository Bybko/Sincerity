using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class WalkAction : ICharacterAction
{
    public bool isActionFinished = false;

    private MemoryObject _connectedObject;
    private NavMeshAgent _navMesh;
    private Brain _brain;


    public WalkAction()
    {
        //у меня сейчас идёт поиск компонентов в действиях через конструкторы в которых GameObject.Find(")
        //я к тому, что может быть так, что плееров будет несколько, а все компоненты будут иниициализироваться как будто у одного
        //это нужно как-то умнее делать. мб кроме обжекта коннектить ещё и плеера
        _navMesh = GameObject.Find("Player").GetComponent<NavMeshAgent>();
        _brain = GameObject.Find("Player").GetComponentInChildren<Brain>();
    }


    public void Action()
    {
        MoveTo();
    }


    private void MoveTo()
    {
        _navMesh.SetDestination(_connectedObject.GetObjectTransform().position);
    }


    public void SelfDelete()
    {
        _connectedObject.SetAction(null);
        _brain.OnActionRemove.Invoke();   
    }


    public void ConnectWithObject(MemoryObject connectedObject) { _connectedObject = connectedObject; }
    public void SetNavMeshAgent(NavMeshAgent agent) { _navMesh = agent; }
}
