using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Message;

public class Subconscious : MonoBehaviour
{
    [SerializeField] private List<AbstractNeed> _charactersNeeds;
    [SerializeField] private PhysicalStatus _physicalStatus;

    private float _hapinnes = 0f;
    private Feeling _feelingAboutObject = new Feeling();


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


    public Feeling FeelingFromTheObject(Goal foreignObject)
    {
        //���� ������ ������������ ��� ���� ������������, ������� ��� ������� �� ������ ��������� ������ ���� ����� ���������� �����������,� �� � ������� 4
        float happinessChange = 0f;
        AbstractNeed mostSeveralNeed = null;

        foreach (AbstractNeed need in _charactersNeeds)
        {
            happinessChange += need.PredictHappinessChange(foreignObject);
            if (mostSeveralNeed != null)
            {
                if (mostSeveralNeed.GetSeverity() < need.GetSeverity()) { mostSeveralNeed = need; }
            }
            else { mostSeveralNeed = need; }
        }
        //�������� ����� �������� ��� ���, �� ��, ��� �������� � ������� ;)
        _feelingAboutObject.SetHappinessChange(happinessChange);
        _feelingAboutObject.SetHealthChange(foreignObject.GetDamageValue());
        _feelingAboutObject.SetFoodChange(foreignObject.GetFoodValue());
        _feelingAboutObject.SetMostNeedSatisfaction(mostSeveralNeed.PredictSatisfactionChange(foreignObject));

        return _feelingAboutObject;
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
