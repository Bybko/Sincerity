using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Message;

public class Subconscious : MonoBehaviour
{
    [SerializeField] private List<AbstractNeed> _charactersNeeds;
    private float _hapinnes = 0f;


    private void Start()
    {
        foreach (AbstractNeed need in _charactersNeeds)
        {
            need.Initialize();
        }
    }


    private void Update()
    {
        //�� ��� ����������� �����-������ ������ ��������, ����� �� ���� ������ ���� ��� �����������
        //��� � ������ ��������, � � ��� ������, ���� ��������� �����-�� �������. ����� ����� ����������� ����������.
        UpdateHapinnes();
    }


    public void CreateFeelings()
    {

    }


    private void UpdateHapinnes()
    {
        //�������� ��� ��������� �������. ����� ���� ��������.
        _hapinnes = 0f;

        foreach (AbstractNeed need in _charactersNeeds)
        {
            need.SatisfactionLevelCalculation();
            _hapinnes += need.NeedResult();
        }
        Debug.Log("�������: " + _hapinnes);
    }
}
