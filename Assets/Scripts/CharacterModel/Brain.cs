using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Brain : MonoBehaviour
{
    public Action OnActionRemove;

    [SerializeField] private Memory _memory;
    [SerializeField] private Subconscious _subconscious;
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


    private void Start()
    {

    }


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
    }


    public void TellAboutReachingObject(ForeignObject goal)
    {

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
        _brainAction.RequestDecision();

        yield return new WaitUntil(() => _isFinalActionReady);

        IsFinalActionReady(false);
    }


    private void AnalizeDecision()
    {
        MemoryObject newGoal = _memory.GetMostWantedObjectWithAction();
        if (newGoal != null)
        {
            _memory.AddNewGoal(newGoal);

            newGoal.GetAction().Action();
        }
    }


    public void IsInstinctsDecisionReady(bool newStatus) { _isInstinctDecisionReady = newStatus; }
    public void IsEmotionalDecisionReady(bool newStatus) { _isEmotionalDecisionReady = newStatus; }
    public void IsFinalDecisionReady(bool newStatus) { _isFinalDecisionReady = newStatus; }
    public void IsFinalActionReady(bool newStatus) { _isFinalActionReady = newStatus; }
}
