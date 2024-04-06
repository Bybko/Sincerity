using System;
using UnityEngine;

public abstract class AbstractNeed : MonoBehaviour
{
    protected float _severity = 0f; //the hierarchy is the same
    protected float _satisfaction = 0f;


    public abstract void SatisfactionLevelCalculation();
    public abstract float PredictHappinessChange(ForeignObject foreignObject);
    public abstract float PredictSatisfactionChange(ForeignObject foreignObject);


    public virtual float NeedResult()
    {
        if (_satisfaction > 0.5f)
        {
            return _satisfaction * (float)Math.Pow(_severity, 2);
        }
        else
        {
            return -((1 - _satisfaction) * (float)Math.Pow(_severity, 2));
        }
    }

    
    protected virtual float PredictNeedResult(float predictableSeverity, float predictableSatisfaction)
    {
        if (_satisfaction > 0.5f)
        {
            return _satisfaction * (float)Math.Pow(_severity, 2);
        }
        else
        {
            return -((1 - _satisfaction) * (float)Math.Pow(_severity, 2));
        }
    }


    public void Initialize() { _severity = UnityEngine.Random.value; }

    public float GetSeverity() { return _severity; }
    public float GetSatisfaction() { return _satisfaction; }
}
