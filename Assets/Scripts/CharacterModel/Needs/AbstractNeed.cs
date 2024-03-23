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
        return  _satisfaction + _severity;
    }

    
    protected virtual float PredictNeedResult(float predictableSeverity, float predictableSatisfaction)
    {
        return predictableSatisfaction + predictableSeverity;
    }


    public void Initialize() { _severity = Random.value; }

    public float GetSeverity() { return _severity; }
    public float GetSatisfaction() { return _satisfaction; }
}
