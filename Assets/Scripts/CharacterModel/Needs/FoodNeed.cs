using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FoodNeed : AbstractNeed
{
    [SerializeField] private PhysicalStatus _physicalStatus;


    public override void SatisfactionLevelCalculation()
    {
        _satisfaction = Mathf.Clamp01(_physicalStatus.GetCurrentFoodResources() 
            / _physicalStatus.GetRequestedFoodResources());

        _severity =  1 - _satisfaction; //Чем больше удовлетворённость потребности в еде, тем меньше её выраженность

        //Debug.Log("Голод. Удовлетворённость: " + _satisfaction);
        //Debug.Log("Голод. Выраженность: " + _severity);
    }


    public override float PredictSatisfactionChange(ForeignObject foreignObject)
    {
        float predictableSatisfaction = Mathf.Clamp01((_physicalStatus.GetCurrentFoodResources() + 
            foreignObject.GetFoodValue()) / _physicalStatus.GetRequestedFoodResources());
        return Math.Abs(_satisfaction - predictableSatisfaction);
    }


    public override float PredictHappinessChange(ForeignObject foreignObject)
    {
        float predictableSatisfaction = Mathf.Clamp01( (_physicalStatus.GetCurrentFoodResources() + 
            foreignObject.GetFoodValue() ) / _physicalStatus.GetRequestedFoodResources());

        float predictableSeverity = 1 - predictableSatisfaction;

        return Math.Abs(NeedResult() - PredictNeedResult(predictableSeverity, _satisfaction)); 
    }


    public override float NeedResult()
    {
        return -1 * _severity;   
    }


    protected override float PredictNeedResult(float predictableSeverity, float predictableSatisfaction)
    {
        return -1 * predictableSeverity;
    }
}
