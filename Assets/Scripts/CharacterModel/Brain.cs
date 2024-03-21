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

    //cringe cringe cringe cringe cringe cringe
    private float _currentDecision = 0f;
    private Transform _bestGoal;


    private void Update()
    {
        if (_bestGoal != null) { _navMesh.SetDestination(_bestGoal.position); }
    }

    //Пока кринжовая проверка на воспоминание, ибо память реализована элементарно от задуманной.
    //Да и в целом функция пока кринжовая
    public IEnumerator AnalizeForeignObjects(Goal foreignObject)
    {
        if (_memory.TryingToRemember(foreignObject))
        {
            //Debug.Log("I remember it!");
        }
        else
        {
            _memory.MemorizeObject(foreignObject);
        }

        Feeling feeling = _subconscious.FeelingFromTheObject(foreignObject);
        _instincts.SetFeeling(feeling);
        _emotions.SetFeeling(feeling);

        yield return StartCoroutine(RequestBrainDecision());

        MakeGoalDecision(foreignObject);
    }


    private IEnumerator RequestBrainDecision()
    {
        _instincts.RequestDecision();
        _emotions.RequestDecision();
        
        yield return new WaitUntil(() => _isEmotionalDecisionReady && _isInstinctDecisionReady);
        
        //reset for next decision
        IsEmotionalDecisionReady(false);
        IsInstinctsDecisionReady(false);
        
        _brainDecision.RequestDecision();

        yield return new WaitUntil(() => _isFinalDecisionReady);

        IsFinalDecisionReady(false);
    }

    //по хорошему это всё говно надо из памяти брать
    private void MakeGoalDecision(Goal foreignObject)
    {
        if (_currentDecision == 0f)
        {
            _currentDecision = _brainDecision.GetFinalDecision();
            _bestGoal = foreignObject.transform;
        }

        if (_currentDecision < _brainDecision.GetFinalDecision())
        {
            _currentDecision = _brainDecision.GetFinalDecision();
            _bestGoal = foreignObject.transform;
        }
    }


    public void IsInstinctsDecisionReady(bool newStatus) { _isInstinctDecisionReady = newStatus; }
    public void IsEmotionalDecisionReady(bool newStatus) { _isEmotionalDecisionReady = newStatus; }
    public void IsFinalDecisionReady(bool newStatus) { _isFinalDecisionReady = newStatus; }
}
