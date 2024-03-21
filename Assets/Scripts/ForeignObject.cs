using UnityEngine;

public class ForeignObject : MonoBehaviour
{
    [SerializeField] private float _foodValue;
    [SerializeField] private float _damageValue;


    public float GetFoodValue() { return _foodValue; }
    public float GetDamageValue() { return _damageValue; }
}
