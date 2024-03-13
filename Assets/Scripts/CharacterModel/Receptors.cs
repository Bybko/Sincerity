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
    [SerializeField] private List<Goal> _foreignObjects;

    //����� ��������� ����������, ����� ������� ���������� ����� ������� ��������.
    //������� �� ����� ������������ ����� � ������, ������� ����� �����, ��� ��������� �������� ������, � ������� �����
    //�����������������, ����� ���� ������� ������ ����� ������� � ��� ��� ���� ����� ������ �����, ��������� ��� ��� ���.
    public void CheckForeignObjects()
    {
        for (int i = 0; i < _foreignObjects.Count; i++)
        {
            _brain.AnalizeForeignObjects(_foreignObjects[i]);
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
