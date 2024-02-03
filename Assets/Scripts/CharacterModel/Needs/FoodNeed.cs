using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodNeed : AbstractNeed
{
    [SerializeField] private PhysicalStatus _physicalStatus;


    public override void SatisfactionLevelCalculation()
    {
        float currentResources = _physicalStatus.GetCurrentFoodResources();
        float requestedResources = _physicalStatus.GetRequestedFoodResources();

        _satisfaction = _physicalStatus.GetCurrentFoodResources() / _physicalStatus.GetRequestedFoodResources();
        _satisfaction = Mathf.Clamp01(_satisfaction); //ограничивает от 0 до 1
    }
}
