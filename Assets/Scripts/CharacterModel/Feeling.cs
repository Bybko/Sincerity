using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feeling
{
    //���� ������ ������������ ��� ���� ������������
    private float _foodSatisfactionChange = 0f;
    private float _healthChange = 0f;
    private float _totalHappinessChange = 0f;
    private float _mostSeveralNeedSatisfaction;


    public float GetHealthChange() {  return _healthChange; }
    public float GetTotalHappinessChange() { return _totalHappinessChange; }
    public float GetFoodChange() { return _foodSatisfactionChange; }
    public float GetMostNeedSatisfaction() { return _mostSeveralNeedSatisfaction; }

    public void SetHealthChange(float healthChange) { _healthChange = healthChange; }
    public void SetFoodChange(float foodSatisfactionChange) { _foodSatisfactionChange = foodSatisfactionChange; }
    public void SetHappinessChange(float totalHappinessChange) { _totalHappinessChange = totalHappinessChange; }
    public void SetMostNeedSatisfaction(float satisfaction) { _mostSeveralNeedSatisfaction = satisfaction; }
}
