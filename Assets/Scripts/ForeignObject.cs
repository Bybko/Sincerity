using UnityEngine;

public class ForeignObject : MonoBehaviour
{
    [SerializeField] private float _foodValue;
    [SerializeField] private float _damageValue;


    public void OnTriggerEnter(Collider other)
    {
        Receptors receptors = other.GetComponent<Receptors>();
        if (receptors != null)
        {
            StartCoroutine(receptors.AddForeignObject(this));
        }
    }


    public void OnTriggerExit(Collider other)
    {
        Receptors receptors = other.GetComponent<Receptors>();
        if (receptors != null)
        {
            StartCoroutine(receptors.DeleteForeignObject(this));
        }
    }


    public float GetFoodValue() { return _foodValue; }
    public float GetDamageValue() { return _damageValue; }
}