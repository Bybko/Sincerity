using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodNeed : AbstractNeed
{
    [SerializeField] private PhysicalStatus _physicalStatus;


    public override void SatisfactionLevelCalculation()
    {
        _satisfaction = _physicalStatus.GetCurrentFoodResources() / _physicalStatus.GetRequestedFoodResources();
        _satisfaction = Mathf.Clamp01(_satisfaction); //ограничивает от 0 до 1

        _severity =  1 - _satisfaction; //Чем больше удовлетворённость потребности в еде, тем меньше её выраженность

        Debug.Log("Удовлетворённость: " + _satisfaction);
        Debug.Log("Выраженность: " + _severity);
    }
}
