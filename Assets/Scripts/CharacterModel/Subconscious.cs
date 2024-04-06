using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subconscious : MonoBehaviour
{
    [SerializeField] private List<AbstractNeed> _charactersNeeds;
    [SerializeField] private PhysicalStatus _physicalStatus;

    private CuriosityNeed _curiosityNeed;
    private float _hapinnes = 0f;


    private void Start()
    {
        foreach (AbstractNeed need in _charactersNeeds)
        {
            need.Initialize();

            if (need is CuriosityNeed)
            {
                _curiosityNeed = (CuriosityNeed)need;
            }
        }
    }


    private void Update()
    {
        StartCoroutine(UpdateHappinesOverTime());
    }


    public Feeling FeelingFromTheObject(ForeignObject foreignObject)
    {
        Feeling _feelingAboutObject = new Feeling();
        float happinessChange = 0f;
        AbstractNeed mostSeveralNeed = null;

        foreach (AbstractNeed need in _charactersNeeds)
        {
            happinessChange += need.PredictHappinessChange(foreignObject);
            if (mostSeveralNeed != null)
            {
                if (mostSeveralNeed.GetSeverity() < need.GetSeverity()) { mostSeveralNeed = need; }
            }
            else { mostSeveralNeed = need; }
        }
        _feelingAboutObject.SetDanger(ForeignObjectDangerCalculate(foreignObject));
        _feelingAboutObject.SetHappinessChange(happinessChange);
        _feelingAboutObject.SetHealthChange(foreignObject.GetDamageValue());
        _feelingAboutObject.SetFoodChange(foreignObject.GetFoodValue());
        _feelingAboutObject.SetMostNeedSatisfaction(mostSeveralNeed.PredictSatisfactionChange(foreignObject));

        return _feelingAboutObject;
    }


    public void ForeignObjectsInfluence(ForeignObject foreignObject)
    {
        _physicalStatus.ChangeFoodResources(foreignObject.GetFoodValue());
        _physicalStatus.ChangeHealth(foreignObject.GetDamageValue());
    }


    public float GetHappines()
    {
        UpdateHapinnes();
        return _hapinnes;
    }


    public float ForeignObjectDangerCalculate(ForeignObject foreignObject)
    {
        float movingValue = 0f;
        if (foreignObject.IsMoving()) { movingValue = 1f; }

        float parametersNum = 3f; 
        return Mathf.Clamp01((foreignObject.GetObjectSize() + (-1 * foreignObject.GetDamageValue()) + movingValue) 
            / parametersNum);
    }


    public void AddDiscoveryAward(ForeignObject foreignObject)
    {
        _curiosityNeed.AddSatisfactionForDiscovery(InterestInForeignObjectCalculate(foreignObject));
    }


    public float InterestInForeignObjectCalculate(ForeignObject foreignObject)
    {
        float movingValue = 0f;
        if (foreignObject.IsMoving()) { movingValue = 1f; }

        float parametersNum = 4f;
        return Mathf.Clamp01((foreignObject.GetObjectSize() + Mathf.Abs(foreignObject.GetDamageValue()) 
            + movingValue + foreignObject.GetFoodValue())
            / parametersNum);
    }


    private IEnumerator UpdateHappinesOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(15f);
            UpdateHapinnes();
        }
    }


    private void UpdateHapinnes()
    {
        _hapinnes = 0f;

        foreach (AbstractNeed need in _charactersNeeds)
        {
            need.SatisfactionLevelCalculation();
            _hapinnes += need.NeedResult();
        }
    }
}
