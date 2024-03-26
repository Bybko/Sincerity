using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Brain : MonoBehaviour
{
    [SerializeField] private Memory _memory;
    [SerializeField] private Subconscious _subconscious;
    [SerializeField] private NavMeshAgent _navMesh;

    [Header("Agents")]
    [SerializeField] private InstinctBrainAgent _instincts;
    [SerializeField] private EmotionalBrainAgent _emotions;
    [SerializeField] private BrainAgent _brainDecision;

    private bool _isEmotionalDecisionReady = false;
    private bool _isInstinctDecisionReady = false;
    private bool _isFinalDecisionReady = false;


    public IEnumerator AnalizeForeignObject(ForeignObject foreignObject)
    {
        MemoryObject rememberedObject = _memory.Remember(foreignObject);
        if (rememberedObject != null) 
        {
            _brainDecision.SetInputs(rememberedObject.GetInstinctDecision(), rememberedObject.GetEmotionalDecision());
            _brainDecision.RequestDecision();

            yield return new WaitUntil(() => _isFinalDecisionReady);

            IsFinalDecisionReady(false);
        }
        else
        {
            Feeling feeling = _subconscious.FeelingFromTheObject(foreignObject);
            _instincts.SetFeeling(feeling);
            _emotions.SetFeeling(feeling);

            yield return StartCoroutine(RequestBrainDecision());

            _memory.MemorizeObject(foreignObject, _instincts.GetInstinctDecision(), 
                _emotions.GetEmotionalDecision(), _brainDecision.GetFinalDecision());
        }

        AnalizeDecision();
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
    }


    private void AnalizeDecision()
    {
        MemoryObject newGoal = _memory.GetMostWantedObject();
        Debug.Log("Goal now is " + (newGoal == null));
        if (newGoal != null)
        {
            _memory.AddNewGoal(newGoal);

            Debug.Log("My goal now is " + _memory.GetMostWantedGoal().GetObjectImage().GetFoodValue());
            _navMesh.SetDestination(_memory.GetMostWantedGoal().GetObjectTransform().position);
        }
    }


    public void IsInstinctsDecisionReady(bool newStatus) { _isInstinctDecisionReady = newStatus; }
    public void IsEmotionalDecisionReady(bool newStatus) { _isEmotionalDecisionReady = newStatus; }
    public void IsFinalDecisionReady(bool newStatus) { _isFinalDecisionReady = newStatus; }
}
