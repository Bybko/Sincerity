using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Subconscious : MonoBehaviour
{
    [SerializeField] private List<AbstractNeed> _charactersNeeds;
    [SerializeField] private PhysicalStatus _physicalStatus;
    [SerializeField] private NavMeshAgent _navmesh;
            
    private CuriosityNeed _curiosityNeed;
    private float _hapinnes = 0f;
    private bool _isWantToSleep = false;


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
        _feelingAboutObject.SetCurrentHealth(_physicalStatus.GetHealth());
        _feelingAboutObject.SetCurrentFoodResources(_physicalStatus.GetCurrentFoodResources());

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


    public float ObjectValueCalculate(ForeignObject foreignObject)
    {
        float parametersNum = 2f;
        return Mathf.Clamp01((foreignObject.GetFoodValue() + foreignObject.GetDamageValue())
            / parametersNum);
    }


    public bool IsWantToSleep()
    {
        float currentEnergy = _physicalStatus.GetCurrentEnergy();
        if (currentEnergy <= 15f)
        {
            _isWantToSleep = true;
        }

        if (currentEnergy > 15f && currentEnergy < 90f && _physicalStatus.IsSleeping())
        {
            _isWantToSleep = false;
            if (_physicalStatus.IsSleeping()) { _isWantToSleep = true; }
        }

        if (currentEnergy > 90f)
        {
            _isWantToSleep = false;
        }

        return _isWantToSleep;
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


    public void FallAsleep()
    {
        _navmesh.isStopped = true; //it works strange, test it later
        _physicalStatus.SetSleepingStatus(true);
    }


    private IEnumerator UpdateHappinesOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(15f);
            UpdateHapinnes();
        }
    }


    public void WakeUp() 
    {
        _navmesh.isStopped = false;
        _physicalStatus.SetSleepingStatus(false); 
    }


    public bool SleepingStatus() { return _physicalStatus.IsSleeping(); }
    public bool SafetySatus() { return _physicalStatus.IsSafe(); }
}
