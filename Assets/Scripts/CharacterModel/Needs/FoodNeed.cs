using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodNeed : AbstractNeed
{
    [SerializeField] private PhysicalStatus _physicalStatus;


    public override void SatisfactionLevelCalculation()
    {
        _satisfaction = _physicalStatus.GetCurrentFoodResources() / _physicalStatus.GetRequestedFoodResources();
        _satisfaction = Mathf.Clamp01(_satisfaction); //������������ �� 0 �� 1

        _severity =  1 - _satisfaction; //��� ������ ���������������� ����������� � ���, ��� ������ � ������������

        Debug.Log("����������������: " + _satisfaction);
        Debug.Log("������������: " + _severity);
    }
}
