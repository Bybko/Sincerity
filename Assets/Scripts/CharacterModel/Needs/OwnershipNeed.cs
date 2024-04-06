using System;
using UnityEngine;

public class OwnershipNeed : AbstractNeed
{
    [SerializeField] private Memory _memory;
    [SerializeField] private Subconscious _subconscious;


    public override void SatisfactionLevelCalculation()
    {
        _satisfaction = _memory.TotalOwnedObjectsValue();
    }


    public override float PredictSatisfactionChange(ForeignObject foreignObject)
    { 
        //игнорится среднее арифметическое, типо все объекты в satisfaction делились на количество, а этот нет
        return Math.Abs(_subconscious.ObjectValueCalculate(foreignObject));
    }


    public override float PredictHappinessChange(ForeignObject foreignObject)
    {
        //игнорится среднее арифметическое, типо все объекты в satisfaction делились на количество, а этот нет
        float predictableSatisfaction = Mathf.Clamp01(_satisfaction 
            + _subconscious.ObjectValueCalculate(foreignObject));
        return Math.Abs(NeedResult() - PredictNeedResult(_severity, predictableSatisfaction));
    }
}
