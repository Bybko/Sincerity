using System;
using UnityEngine;

public class ComfortNeed : AbstractNeed
{
    [SerializeField] private PhysicalStatus _physicalStatus;
    [SerializeField] private Receptors _receptors;


    public override void SatisfactionLevelCalculation()
    {
        _satisfaction = CalculateSatisfaction();
        _severity = 1 - _satisfaction;
    }


    public override float PredictSatisfactionChange(ForeignObject foreignObject)
    {
        return Math.Abs(_satisfaction - PredictSatisfaction(foreignObject));   
    }


    public override float PredictHappinessChange(ForeignObject foreignObject)
    {
        float predictableSeverity = 1 - PredictSatisfaction(foreignObject);
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


    private float PredictSatisfaction(ForeignObject foreignObject)
    {
        float predictableHPSatisfaction = Mathf.Clamp01((_physicalStatus.GetHealth() + foreignObject.GetDamageValue())
            / 100f);
        float dangerSatisfaction = 1 - _receptors.EnvironmentDanger();
        float energySatisfaction = _physicalStatus.GetCurrentEnergy();
        float predictableHungerSatisfaction = Mathf.Clamp01((_physicalStatus.GetCurrentFoodResources() +
            foreignObject.GetFoodValue()) / _physicalStatus.GetRequestedFoodResources());

        if (predictableHPSatisfaction < 0.2) { return predictableHPSatisfaction; }
        if (predictableHungerSatisfaction < 0.1) { return predictableHungerSatisfaction; }
        if (dangerSatisfaction < 0.2) { return dangerSatisfaction; }
        if (energySatisfaction < 0.1) { return energySatisfaction; }


        float parametersNum = 4;
        return Mathf.Clamp01((predictableHPSatisfaction + dangerSatisfaction + energySatisfaction
            + predictableHungerSatisfaction) / parametersNum);
    }


    private float CalculateSatisfaction()
    {
        float hpSatisfaction = Mathf.Clamp01(_physicalStatus.GetHealth() / 100f);
        float dangerSatisfaction = 1 - _receptors.EnvironmentDanger();
        float energySatisfaction = _physicalStatus.GetCurrentEnergy();
        float hungerSatisfaction = Mathf.Clamp01(_physicalStatus.GetCurrentFoodResources()
            / _physicalStatus.GetRequestedFoodResources());

        if (hpSatisfaction < 0.2) { return hpSatisfaction; }
        if (hungerSatisfaction < 0.1) { return hungerSatisfaction; }
        if (dangerSatisfaction < 0.2) { return dangerSatisfaction; }
        if (energySatisfaction < 0.1) { return energySatisfaction; }


        float parametersNum = 4;
        return Mathf.Clamp01((hpSatisfaction + dangerSatisfaction + energySatisfaction
            + hungerSatisfaction) / parametersNum);
    }
}
