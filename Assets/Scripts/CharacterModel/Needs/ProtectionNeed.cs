using System;
using UnityEngine;

public class ProtectionNeed : AbstractNeed
{
    [SerializeField] private Memory _memory;


    public override void SatisfactionLevelCalculation()
    {
        _satisfaction = _memory.OwnedObjectsHealthStatus();
    }


    public override float PredictSatisfactionChange(ForeignObject foreignObject)
    {
        float foreignObjectHP = Mathf.Clamp01(foreignObject.GetObjectHP() / 100f);

        if (foreignObjectHP > _satisfaction)
        {
            return 0f;
        }
        
        return foreignObjectHP - _satisfaction; //would be negative difference, it's correct
    }


    public override float PredictHappinessChange(ForeignObject foreignObject)
    {
        float predictableSatisfaction = _satisfaction;

        float foreignObjectHP = Mathf.Clamp01(foreignObject.GetObjectHP() / 100f);
        if (foreignObjectHP < _satisfaction)
        {
            predictableSatisfaction = foreignObjectHP;
        }

        return Math.Abs(NeedResult() - PredictNeedResult(_severity, predictableSatisfaction));
    }
}
