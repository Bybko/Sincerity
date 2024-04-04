using System.Collections;
using UnityEngine;

public class PhysicalStatus : MonoBehaviour
{
    //rebalance this parameters for a long game after learning
    [SerializeField] private float _health = 100f;
    [SerializeField] private float _foodEnergySpending = 5f;
    [SerializeField] private float _requestedFoodResources = 100f;
    [SerializeField] private float _energySpending = 0.1f;
    [SerializeField] private float _requestedEnergy = 100f;

    private float _currentFoodResources;
    private float _currentEnergy;


    private void Start()
    {
        _currentFoodResources = _requestedFoodResources;
        _currentEnergy = _requestedEnergy;

        StartCoroutine(DecreaseValuesOverTime());
    }


    public void SetRandomValues()
    {
        _currentFoodResources = Random.Range(0f, 50f);
        _health = Random.Range(15f, 50f);
    }


    public void ChangeFoodResources(float foodValue) { _currentFoodResources += foodValue; }
    public void ChangeHealth(float hpValue) { _health += hpValue; }
    public void ChangeEnergy(float energyValue) {  _currentEnergy += energyValue; }


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
        if (_currentFoodResources < 10f)
        {
            _health = Mathf.Clamp(_health - tiredDamage, 0f, 100f);
        }
    }


    public float GetCurrentFoodResources() { return _currentFoodResources; }
    public float GetRequestedFoodResources() { return _requestedFoodResources; }
    public float GetHealth() { return _health; }
    public float GetCurrentEnergy() {  return _currentEnergy; }
    public float GetRequestedEnergy() {  return _requestedEnergy; }
}
