using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Subconscious : MonoBehaviour
{
    [SerializeField] private List<AbstractNeed> _charactersNeeds;
    [SerializeField] private PhysicalStatus _physicalStatus;

    private float _hapinnes = 0f;


    private void Start()
    {
        foreach (AbstractNeed need in _charactersNeeds)
        {
            need.Initialize();
        }
    }

    //this doesn't need in training
    /*private void Update()
    {
        //Мб для оптимизации какой-нибудь таймер добавить, чтобы не прям каждый кадр это выполнялось
        //Раз в минуту например, и в том случае, если изменение какое-то внешнее. Чтобы сразу происходило обновление.
        UpdateHapinnes();
    }*/


    public Feeling FeelingFromTheObject(ForeignObject foreignObject)
    {
        Feeling _feelingAboutObject = new Feeling();
        //Пока только адаптировано для двух потребностей, поэтому как минимум на эмоцию поступает только одна самая выраженная потребность,а не к примеру 4
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
        //Возможно стоит задумать над тем, то ли, что задумано я передаю ;)
        _feelingAboutObject.SetHappinessChange(happinessChange);
        _feelingAboutObject.SetHealthChange(foreignObject.GetDamageValue());
        _feelingAboutObject.SetFoodChange(foreignObject.GetFoodValue());
        _feelingAboutObject.SetMostNeedSatisfaction(mostSeveralNeed.PredictSatisfactionChange(foreignObject));

        return _feelingAboutObject;
    }


    public void ForeignObjectsInfluence(ForeignObject foreignObject)
    {
        _physicalStatus.ChangeFoodResources(foreignObject.GetFoodValue());
        _physicalStatus.ChangeHealth(foreignObject.GetDamageValue());
    }


    public float GetHappines()
    {
        UpdateHapinnes();
        return _hapinnes;
    }


    private void UpdateHapinnes()
    {
        //Подумать над рассчётом счастья. Может быть изменить.
        _hapinnes = 0f;

        foreach (AbstractNeed need in _charactersNeeds)
        {
            need.SatisfactionLevelCalculation();
            _hapinnes += need.NeedResult();
        }
        //Debug.Log("Счастье: " + _hapinnes);
    }
}
