using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    private void UpdateHapinnes()
    {
        //�������� ��� ��������� �������. ����� ���� ��������.
        _hapinnes = 0f;

        foreach (AbstractNeed need in _charactersNeeds)
        {
            need.SatisfactionLevelCalculation();
            _hapinnes += need.NeedResult();
        }
    }

    // ������ �����. ������� ����� ����� ������ ��� ������.
    public void ChangeHapinnes(float changeValue)
    {
        _hapinnes += changeValue;
    }
}
