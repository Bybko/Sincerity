public class Feeling
{
    //Right now is only adapt for two needs
    private float _foodSatisfactionChange = 0f;
    private float _healthChange = 0f;
    private float _totalHappinessChange = 0f;
    private float _mostSeveralNeedSatisfaction = 0f;
    private float _danger = 0f;
    private float _currentFoodResources = 0f;
    private float _currentHealth = 0f;


    public float GetHealthChange() {  return _healthChange; }
    public float GetTotalHappinessChange() { return _totalHappinessChange; }
    public float GetFoodChange() { return _foodSatisfactionChange; }
    public float GetMostNeedSatisfaction() { return _mostSeveralNeedSatisfaction; }
    public float GetDanger() { return _danger; }
    public float GetCurrentFoodResources() { return _currentFoodResources; }
    public float GetCurrentHealth() { return _currentHealth; }

    public void SetHealthChange(float healthChange) { _healthChange = healthChange; }
    public void SetFoodChange(float foodSatisfactionChange) { _foodSatisfactionChange = foodSatisfactionChange; }
    public void SetHappinessChange(float totalHappinessChange) { _totalHappinessChange = totalHappinessChange; }
    public void SetMostNeedSatisfaction(float satisfaction) { _mostSeveralNeedSatisfaction = satisfaction; }
    public void SetDanger(float  danger) { _danger = danger;}
    public void SetCurrentFoodResources(float food) { _currentFoodResources = food; }
    public void SetCurrentHealth(float health) { _currentHealth = health; }
}
