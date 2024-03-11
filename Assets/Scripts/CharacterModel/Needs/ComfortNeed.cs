using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Расширить потом скрипт, он должен ещё оценивать уровень энергии, оценки безопасности и прочее связанное с этой потребностью
//Кстати! По-идее на безопасность тоже в какой-то мере должен влиять и уровень сытости!
public class ComfortNeed : AbstractNeed
{
    [SerializeField] private PhysicalStatus _physicalStatus;


    public override void SatisfactionLevelCalculation()
    {
        _satisfaction = Mathf.Clamp01(_physicalStatus.GetHealth() / 100f);

        _severity = 1 - _satisfaction; //Пока просто скопировано у голода, т.к. нет других факторов, кроме хп

        //Debug.Log("Безопасность. Удовлетворённость: " + _satisfaction);
        //Debug.Log("Безопасность. Выраженность: " + _severity);
    }


    public override float PredictSatisfactionChange(Goal foreignObject)
    {
        float predictableSatisfaction = Mathf.Clamp01((_physicalStatus.GetHealth() + foreignObject.GetDamageValue())
            / 100f);
        return Math.Abs(_satisfaction - predictableSatisfaction);   
    }


    public override float PredictHappinessChange(Goal foreignObject)
    {
        float predictableSatisfaction = Mathf.Clamp01((_physicalStatus.GetHealth() + foreignObject.GetDamageValue()) 
            / 100f);

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
