using System;
using System.Collections;
using System.Security.Cryptography;
using System.Xml.Serialization;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class Brain : MonoBehaviour
{
    public Action OnActionRemove;

    [SerializeField] private Memory _memory;
    [SerializeField] private Subconscious _subconscious;
    [SerializeField] private PhysicalStatus _physicalStatus;
    [SerializeField] private NavMeshAgent _navMesh;

    [Header("Agents")]
    [SerializeField] private InstinctBrainAgent _instincts;
    [SerializeField] private EmotionalBrainAgent _emotions;
    [SerializeField] private BrainAgent _brainDecision;
    [SerializeField] private BrainActionAgent _brainAction;

    private bool _isEmotionalDecisionReady = false;
    private bool _isInstinctDecisionReady = false;
    private bool _isFinalDecisionReady = false;
    private bool _isFinalActionReady = false;

    private Vector3 _searchingPosition;
    private bool _isSearching = false;

    
    private void Update()
    {
        //maybe too heavy for every frame update
        AnalizeDecision();
    }


    public IEnumerator AnalizeForeignObject(ForeignObject foreignObject)
    {
        MemoryObject rememberedObject = _memory.Remember(foreignObject);
        if (rememberedObject == null)
        {
            _subconscious.AddDiscoveryAward(foreignObject);

            Feeling feeling = _subconscious.FeelingFromTheObject(foreignObject);
            _instincts.SetFeeling(feeling);
            _emotions.SetFeeling(feeling);

            yield return StartCoroutine(RequestBrainDecision());

            _memory.MemorizeObject(foreignObject, _instincts.GetInstinctDecision(), 
                _emotions.GetEmotionalDecision(), _brainDecision.GetFinalDecision());
            _memory.SetNewAction(foreignObject, _brainAction.GetAction());
        }
        else
        {
            yield return StartCoroutine(RequestBrainAction(rememberedObject));
        }
    }


    public void TellAboutReachingObject(ForeignObject goal)
    {
        _memory.ReachObject(goal);
    }

    
    public void ResetMemory()
    {
        _memory.ClearLists();
    }


    public void StopMoving()
    {
        _navMesh.isStopped = true;
    }



    private IEnumerator RequestBrainDecision()
    {
        _instincts.RequestDecision();
        _emotions.RequestDecision();

        yield return new WaitUntil(() => _isEmotionalDecisionReady && _isInstinctDecisionReady);

        //reset for next decision
        IsEmotionalDecisionReady(false);
        IsInstinctsDecisionReady(false);

        _brainDecision.SetInputs(_instincts.GetInstinctDecision(), _emotions.GetEmotionalDecision());
        _brainDecision.RequestDecision();

        yield return new WaitUntil(() => _isFinalDecisionReady);

        IsFinalDecisionReady(false);


        _brainAction.SetInputs(_instincts.GetInstinctDecision(), _emotions.GetEmotionalDecision(),
            _brainDecision.GetFinalDecision());
        _brainAction.SetCurrentForeignObject(_physicalStatus.GetCurrentForeignObject());
        _brainAction.RequestDecision();

        yield return new WaitUntil(() => _isFinalActionReady);
        Debug.Log("ACTIOOOOOOON");

        IsFinalActionReady(false);
    }


    private IEnumerator RequestBrainAction(MemoryObject rememberedObject)
    {
        _brainAction.SetInputs(rememberedObject.GetInstinctDecision(), rememberedObject.GetEmotionalDecision(),
            rememberedObject.GetFinalDecision());
        _brainAction.SetCurrentForeignObject(_physicalStatus.GetCurrentForeignObject());
        _brainAction.RequestDecision();

        yield return new WaitUntil(() => _isFinalActionReady);
        Debug.Log("Remembered object reaction");

        IsFinalActionReady(false);
    }


    private void AnalizeDecision()
    {
        if (_subconscious.IsWantToSleep() && !_subconscious.SleepingStatus())
        {
            Debug.Log("I'm sleeping...");
            StartCoroutine(Sleep());
        }

        if (!_subconscious.SleepingStatus())
        {
            MemoryObject newGoal = _memory.GetMostWantedObjectWithAction();
            if (newGoal != null)
            {
                _isSearching = false;
                _memory.AddNewGoal(newGoal);
                newGoal.GetAction().Action();
            }
            else if(_isSearching == false || _searchingPosition == gameObject.transform.position) 
            { 
                Debug.Log("I'm searching..."); 
                Search(); 
            }
        }
    }


    private IEnumerator Sleep()
    {
        _subconscious.FallAsleep();

        float energyRecovery = 10f;
        do
        {
            yield return new WaitForSeconds(5);
            _physicalStatus.ChangeEnergy(energyRecovery);

        } while (_subconscious.SafetySatus() && _subconscious.IsWantToSleep());

        _subconscious.WakeUp();
    }


    private void Search()
    {
        _isSearching = true;

        float searchRadius = 10f;
        float randomX = transform.position.x + UnityEngine.Random.Range(-searchRadius, searchRadius);
        float randomZ = transform.position.z + UnityEngine.Random.Range(-searchRadius, searchRadius);
        Vector3 randomPosition = new Vector3(randomX, transform.position.y, randomZ);
        _searchingPosition = randomPosition;

        //maybe later if navmesh areas would be more complicated
        //NavMeshHit hit;
        //if (NavMesh.SamplePosition(randomPosition, out hit, searchRadius, NavMesh.AllAreas))
        _navMesh.SetDestination(randomPosition);
    }


    public void ResetSearchStatus() { _isSearching = false; }
    public void IsInstinctsDecisionReady(bool newStatus) { _isInstinctDecisionReady = newStatus; }
    public void IsEmotionalDecisionReady(bool newStatus) { _isEmotionalDecisionReady = newStatus; }
    public void IsFinalDecisionReady(bool newStatus) { _isFinalDecisionReady = newStatus; }
    public void IsFinalActionReady(bool newStatus) { _isFinalActionReady = newStatus; }
}
