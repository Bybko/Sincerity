using System.Collections;
using UnityEngine;

public class PhysicalStatus : MonoBehaviour
{
    [SerializeField] private Receptors _receptors;
    //rebalance this parameters for a long game after learning
    [SerializeField] private float _health = 100f;
    [SerializeField] private float _foodEnergySpending = 5f;
    [SerializeField] private float _requestedFoodResources = 100f;
    [SerializeField] private float _energySpending = 0.1f;
    [SerializeField] private float _requestedEnergy = 100f;
    [SerializeField] private float _safetyTimer = 5f; //set 20f after
    [SerializeField] private float _damagePotential = -100f; //set -20f after, не надо шоб с одного удара выносил
    [SerializeField] private float _healPotential = 100f; //set 20f after

    private float _currentFoodResources;
    private float _currentEnergy;
    private float _lastDamageTime = 0f;
    private bool _isSleeping = false;
    private bool _isSafe = true;


    private void Start()
    {
        _currentFoodResources = _requestedFoodResources;
        _currentEnergy = _requestedEnergy;

        StartCoroutine(DecreaseValuesOverTime());
    }


    private void Update()
    {
        //idk is it good to use Time. think about it later
        if (Time.time - _lastDamageTime > _safetyTimer)
        {
            _isSafe = true;
        }
    }


    public void SetRandomValues()
    {
        _currentFoodResources = Random.Range(0f, 50f);
        _health = Random.Range(15f, 50f);
    }


    public void ChangeFoodResources(float foodValue) 
    { _currentFoodResources = Mathf.Clamp(_currentFoodResources + foodValue, 0f, 100f); }

    public void ChangeEnergy(float energyValue) 
    {  _currentEnergy = Mathf.Clamp(_currentEnergy + energyValue, 0f, 100f); }

    public void ChangeHealth(float hpValue) 
    {
        if (hpValue < 0f) { TakeDamadge(); }
        _health = Mathf.Clamp(_health + hpValue, 0f, 100f);
    }


    private IEnumerator DecreaseValuesOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            DecreaseEnergy();
            DecreaseFood();
        }
    }


    private void DecreaseFood()
    {
        _currentFoodResources = Mathf.Clamp(_currentFoodResources - _foodEnergySpending, 0f, _requestedFoodResources);

        float hungerDamage = 5f;
        if (_currentFoodResources < 10f)
        {
            TakeDamadge();
            _health = Mathf.Clamp(_health - hungerDamage, 0f, 100f);
        }

        if (_currentFoodResources > 90f)
        {
            _health = Mathf.Clamp(_health + hungerDamage, 0f, 100f);
        }
    }


    private void DecreaseEnergy()
    {
        _currentEnergy = Mathf.Clamp(_currentEnergy - _energySpending, 0f, _requestedEnergy);

        float tiredDamage = 5f;
        if (_currentEnergy < 10f)
        {
            //there is no need to use TakeDamage here bc character NEED sleep
            _health = Mathf.Clamp(_health - tiredDamage, 0f, 100f);
        }
    }


    private void TakeDamadge()
    {
        _lastDamageTime = Time.time;
        _isSafe = false;
    }


    public void SetSleepingStatus(bool newStatus) { _isSleeping = newStatus; }

    public float GetCurrentFoodResources() { return _currentFoodResources; }
    public float GetRequestedFoodResources() { return _requestedFoodResources; }
    public float GetHealth() { return _health; }
    public float GetPotentialDamage() { return _damagePotential; }
    public float GetPotentialHeal() { return _healPotential; }
    public float GetCurrentEnergy() {  return _currentEnergy; }
    public float GetRequestedEnergy() {  return _requestedEnergy; }
    public ForeignObject GetCurrentForeignObject() { return _receptors.GetCurrentInteractObject(); }
    public bool IsSleeping() { return _isSleeping; }
    public bool IsSafe() { return _isSafe; }
}
