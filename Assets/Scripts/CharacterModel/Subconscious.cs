using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subconscious : MonoBehaviour
{
    [SerializeField] private List<AbstractNeed> _charactersNeeds;
    [SerializeField] private PhysicalStatus _physicalStatus;

    private float _hapinnes = 0f;


    private void Start()
    {
        foreach (AbstractNeed need in _charactersNeeds)
        {
            need.Initialize();
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
