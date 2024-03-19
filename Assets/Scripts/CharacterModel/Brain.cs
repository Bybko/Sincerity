using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro.EditorUtilities;
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

        Feeling feeling = _subconscious.FeelingFromTheObject(foreignObject); //потом заменить это и сразу в функции передавать метод
        _instincts.SetFeeling(feeling);
        _emotions.SetFeeling(feeling);
        //Debug.Log("Food Change: " + feeling.GetFoodChange());
        //Debug.Log("Health Change: " + feeling.GetHealthChange());
        Debug.Log("Need Change: " + feeling.GetMostNeedSatisfaction());
        Debug.Log("Total Change: " + feeling.GetTotalHappinessChange()); 
        yield return StartCoroutine(RequestBrainDecision());
        //Debug.Log("Instincts: " + _instincts.GetInstinctDecision());
        //Debug.Log("Feelings: " + _emotions.GetEmotionalDecision());
        //Debug.Log(_brainDecision.GetFinalDecision());
        MakeGoalDecision(foreignObject);
        //Debug.Log("Request is over");
    }


    private IEnumerator RequestBrainDecision()
    {
        Debug.Log("Before Request" + _emotions.GetEmotionalDecision());
        _instincts.RequestDecision();
        _emotions.RequestDecision();
        
        yield return new WaitUntil(() => _isEmotionalDecisionReady && _isInstinctDecisionReady);
        
        //reset for next decision
        IsEmotionalDecisionReady(false);
        IsInstinctsDecisionReady(false);
        
        Debug.Log("After Request" + _emotions.GetEmotionalDecision());
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
