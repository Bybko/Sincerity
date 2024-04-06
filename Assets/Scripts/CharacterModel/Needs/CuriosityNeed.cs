using System;
using System.Collections;
using UnityEngine;

public class CuriosityNeed : AbstractNeed
{
    //primitive realization
    [SerializeField] private Subconscious _subconscious;


    private void Start()
    {
        StartCoroutine(DecreaseSatisfaction());
    }


    public override void SatisfactionLevelCalculation()
    {
        return;
    }


    public override float PredictSatisfactionChange(ForeignObject foreignObject)
    {
        return Math.Abs(_subconscious.InterestInForeignObjectCalculate(foreignObject));
    }


    public void AddSatisfactionForDiscovery(float discoveryValue)
    {
        _satisfaction = Mathf.Clamp01(_satisfaction + discoveryValue);
    }


    public override float PredictHappinessChange(ForeignObject foreignObject)
    {
        float predictableSatisfaction = Mathf.Clamp01(_satisfaction + 
            _subconscious.InterestInForeignObjectCalculate(foreignObject));

        return Math.Abs(NeedResult() - PredictNeedResult(_severity, predictableSatisfaction));
    }


    private IEnumerator DecreaseSatisfaction()
    {
        while (true) 
        {
            yield return new WaitForSeconds(5f);

            _satisfaction -= 0.01f;
        }
    }
}
