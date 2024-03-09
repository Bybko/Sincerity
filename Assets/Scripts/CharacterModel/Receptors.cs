using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����� ����� ������������ ���������� ����������, � ���� ��� ���
public class Receptors : MonoBehaviour
{
    [SerializeField] private Brain _brain;
    // ���� � ���� ��������� ������ � ��������, �� ���� �� ����� ������� �� ���� ��������� ������))))
    [SerializeField] private List<Goal> _goals;

    //����� ��������� ����������, ����� ������� ���������� ����� ������� ��������.
    //������� �� ����� ������������ ����� � ������, ������� ����� �����, ��� ��������� �������� ������, � ������� �����
    //�����������������, ����� ���� ������� ������ ����� ������� � ��� ��� ���� ����� ������ �����, ��������� ��� ��� ���.
    private void Start()
    {
        for (int i = 0; i < _goals.Count; i++)
        {
            _brain.AnalizeForeignObjects(_goals[i]);
        }
    }
}
