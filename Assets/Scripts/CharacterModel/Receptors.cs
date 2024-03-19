using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����� ����� ������������ ���������� ����������, � ���� ��� ���
public class Receptors : MonoBehaviour
{
    [SerializeField] private Brain _brain;
    [SerializeField] private Subconscious _subconscious;
    [SerializeField] private BrainAgent _brainAgent; //for training
    [SerializeField] private EmotionalBrainAgent _emotions; //for debugging temporary
    [SerializeField] private InstinctBrainAgent _instinct; //for debugging temporary
    // ���� � ���� ��������� ������ � ��������, �� ���� �� ����� ������� �� ���� ��������� ������))))
    [SerializeField] private List<Goal> _foreignObjects;


    private void Start()
    {
        //CheckForeignObjects();
    }
    //����� ��������� ����������, ����� ������� ���������� ����� ������� ��������.
    //������� �� ����� ������������ ����� � ������, ������� ����� �����, ��� ��������� �������� ������, � ������� �����
    //�����������������, ����� ���� ������� ������ ����� ������� � ��� ��� ���� ����� ������ �����, ��������� ��� ��� ���.
    public IEnumerator CheckForeignObjects()
    {
        int i = 0;
        foreach (Goal foreignObject in _foreignObjects)
        {
            Debug.Log("Send object " + i);
            yield return StartCoroutine(_brain.AnalizeForeignObjects(foreignObject));
            Debug.Log("Emotions about object " + i + " are " + _emotions.GetEmotionalDecision());
            //Debug.Log("Instincts about object " + i + " are " + _instinct.GetInstinctDecision());
            //Debug.Log("I would like object " + i + " for " + _brainAgent.GetFinalDecision());
            i++;
        }
    }
    //������ �� �� ���� ����� ������� �� ������ �����, ����� ��� ����� �������� �� ����� ����������

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Goal>(out Goal goal)) 
        {
            _subconscious.ForeignObjectsInfluence(goal);
            _brainAgent.CheckTrainEpisode();
        }
    }
}
