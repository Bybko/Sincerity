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
        //Мб для оптимизации какой-нибудь таймер добавить, чтобы не прям каждый кадр это выполнялось
        //Раз в минуту например, и в том случае, если изменение какое-то внешнее. Чтобы сразу происходило обновление.
        UpdateHapinnes();
    }


    public void CreateFeelings()
    {

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
