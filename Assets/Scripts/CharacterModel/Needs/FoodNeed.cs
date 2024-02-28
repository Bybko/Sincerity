using System.Collections;
using System.Collections.Generic;
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

    public override float NeedResult()
    {
        return -1 * _severity;   
    }
}
