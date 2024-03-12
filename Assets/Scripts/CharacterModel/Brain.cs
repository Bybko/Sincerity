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

    //cringe cringe cringe cringe cringe cringe
    private float _currentDecision = 0f;

    //Пока кринжовая проверка на воспоминание, ибо память реализована элементарно от задуманной.
    //Да и в целом функция пока кринжовая
    public void AnalizeForeignObjects(Goal foreignObject)
    {
        if (_memory.TryingToRemember(foreignObject))
        {
            Debug.Log("I remember it!");
        }
        else
        {
            _memory.MemorizeObject(foreignObject);
        }

        Feeling feeling = _subconscious.FeelingFromTheObject(foreignObject);
        _instincts.SetFeeling(feeling);
        _emotions.SetFeeling(feeling);

        RequestBrainDecision();

        MakeGoalDecision(foreignObject);
    }


    private void RequestBrainDecision()
    {
        _instincts.RequestDecision();
        _emotions.RequestDecision();
        _brainDecision.RequestDecision();
    }


    private void MakeGoalDecision(Goal foreignObject)
    {
        if (_currentDecision == 0f)
        {
            _currentDecision = _brainDecision.GetFinalDecision();
            _navMesh.SetDestination(foreignObject.transform.position);
        }

        if (_currentDecision < _brainDecision.GetFinalDecision())
        {
            _currentDecision = _brainDecision.GetFinalDecision();
            _navMesh.SetDestination(foreignObject.transform.position);
        }
    }
}
