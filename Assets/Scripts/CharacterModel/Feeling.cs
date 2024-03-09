using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feeling
{
    //Пока только адаптировано для двух потребностей
    private float _foodSatisfactionChange = 0f;
    private float _healthChange = 0f;
    private float _totalHappinessChange = 0f;

    public float GetHealthChange() {  return _healthChange; }
    public float GetTotalHappinessChange() { return _totalHappinessChange; }
    public float GetFoodChange() { return _foodSatisfactionChange; }


    public void SetHealthChange(float healthChange) { _healthChange = healthChange; }
    public void SetFoodChange(float foodSatisfactionChange) { _foodSatisfactionChange = foodSatisfactionChange; }
    public void SetHappinessChange(float totalHappinessChange) { _totalHappinessChange = totalHappinessChange; }
}
