using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

//���������� � ���, ����� ������ ������ ��� ����� ������������, ������ ������������ � ����� ������ �� �����. ���� ������� � ������ ������� ��, ��� ������� � ��������� � ������������
public abstract class AbstractNeed : MonoBehaviour
{
    protected float _severity = 0f; //�� ����� � ��� ����� ��������� ���� ��������
    protected float _satisfaction = 0f;


    public abstract void SatisfactionLevelCalculation();


    public abstract float PredictHappinessChange(Goal foreignObject);


    public void Initialize()
    {
        _severity = Random.value;
    }

    //������ ������� ������������ ����� ������-�� ����������
    // ���� �����������������, ��� ��� ������� ������ ������� ������������� � �������. ������� ��������� ������������ ������
    // ����������� ����� ������ � ������ �������.
    /*public void ChangeSeverity(float changeValue)
    {
        _severity += changeValue;
    }*/


    //�������� �������� ������� ��� ��������� �� ������ �����������
    public virtual float NeedResult()
    {
        //������� ������ ����� ����� ��������, ����� ��� ��������� ������ �� ���������. ����� ���� ������� ������� ������������.
        return  _satisfaction * (_severity * 100);
    }

    
    protected virtual float PredictNeedResult(float predictableSeverity, float predictableSatisfaction)
    {
        return predictableSatisfaction * (predictableSeverity * 100);
    }
}
