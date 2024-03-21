using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����� ����� ������������ ���������� ����������, � ���� ��� ���
public class Receptors : MonoBehaviour
{
    [SerializeField] private Brain _brain;
    [SerializeField] private Subconscious _subconscious;
    [SerializeField] private BrainAgent _brainAgent; //for training
    // ���� � ���� ��������� ������ � ��������, �� ���� �� ����� ������� �� ���� ��������� ������))))
    [SerializeField] private List<ForeignObject> _foreignObjects;


    private void Start()
    {
        //CheckForeignObjects();
    }
    //����� ��������� ����������, ����� ������� ���������� ����� ������� ��������.
    //������� �� ����� ������������ ����� � ������, ������� ����� �����, ��� ��������� �������� ������, � ������� �����
    //�����������������, ����� ���� ������� ������ ����� ������� � ��� ��� ���� ����� ������ �����, ��������� ��� ��� ���.
    public IEnumerator CheckForeignObjects()
    {
        foreach (ForeignObject foreignObject in _foreignObjects)
        {
            yield return StartCoroutine(_brain.AnalizeForeignObjects(foreignObject));
        }
    }
    //������ �� �� ���� ����� ������� �� ������ �����, ����� ��� ����� �������� �� ����� ����������

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ForeignObject>(out ForeignObject goal)) 
        {
            _subconscious.ForeignObjectsInfluence(goal);
            _brainAgent.CheckTrainEpisode();
        }
    }
}
