using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//not used yet, needs improvement
public class ActionPlan : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMesh; //for transmission to interfaces 

    private List<ICharacterAction> actions = new List<ICharacterAction>();
}
