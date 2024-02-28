using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private float _foodValue;
    [SerializeField] private float _safetyValue;

    //�������� ��� ����� ���� ����� ��������, ��� ��� �����������. � �� �� ���� �� � ��� ���� ���������� ������� ������. �� ������ �������.
    public float GetFoodValue()
    {
        return _foodValue;
    }


    public float GetSafetyValue()
    {
        return _safetyValue;
    }
}
