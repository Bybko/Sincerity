using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��������� ����� ������, �� ������ ��� ��������� ������� �������, ������ ������������ � ������ ��������� � ���� ������������
//������! ��-���� �� ������������ ���� � �����-�� ���� ������ ������ � ������� �������!
public class SafetyNeed : AbstractNeed
{
    [SerializeField] private PhysicalStatus _physicalStatus;


    public override void SatisfactionLevelCalculation()
    {
        _satisfaction = Mathf.Clamp01(_physicalStatus.GetHealth() / 100f);

        _severity = 1 - _satisfaction; //���� ������ ����������� � ������, �.�. ��� ������ ��������, ����� ��

        Debug.Log("������������. ����������������: " + _satisfaction);
        Debug.Log("������������. ������������: " + _severity);
    }
}
