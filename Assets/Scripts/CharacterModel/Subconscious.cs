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
        //Мб для оптимизации какой-нибудь таймер добавить, чтобы не прям каждый кадр это выполнялось
        //Раз в минуту например, и в том случае, если изменение какое-то внешнее. Чтобы сразу происходило обновление.
        UpdateHapinnes();
    }


    public Feeling FeelingFromTheObject(Goal foreignObject)
    {
        //Пока только адаптировано для двух потребностей
        float happinessChange = 0f;
        foreach (AbstractNeed need in _charactersNeeds)
        {
            happinessChange += need.PredictHappinessChange(foreignObject);
        }
        //Возможно стоит задумать над тем, то ли, что задумано я передаю ;)
        _feelingAboutObject.SetHappinessChange(happinessChange);
        _feelingAboutObject.SetHealthChange(foreignObject.GetDamageValue());
        _feelingAboutObject.SetFoodChange(foreignObject.GetFoodValue());

        return _feelingAboutObject;
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
        Debug.Log("Счастье: " + _hapinnes);
    }
}
