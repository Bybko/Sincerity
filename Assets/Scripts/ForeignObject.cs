using UnityEngine;

public class ForeignObject : MonoBehaviour
{
    [SerializeField] private float _foodValue;
    [SerializeField] private float _damageValue;
    //make parameters bellow formed by Transform scale and NavMesh moving
    [SerializeField] private float _size;
    [SerializeField] private bool _isMoving;

    [SerializeField] private bool _isOwned;
    [SerializeField] private float _objectHP;


    public void OnTriggerEnter(Collider other)
    {
        Receptors receptors = other.GetComponent<Receptors>();
        if (receptors != null && other.isTrigger)
        {
            StartCoroutine(receptors.AddForeignObject(this));
        }
    }


    public void OnTriggerExit(Collider other)
    {
        Receptors receptors = other.GetComponent<Receptors>();
        if (receptors != null && other.isTrigger)
        {
            StartCoroutine(receptors.DeleteForeignObject(this));
        }
    }


    public bool IsMoving() { return _isMoving; }

    public float GetFoodValue() { return _foodValue; }
    public float GetDamageValue() { return _damageValue; }
    public float GetObjectSize() { return _size; }
    public bool GetOwnedStatus() { return _isOwned; }
    public float GetObjectHP() { return _objectHP; }

    public void SetOwnedStatus(bool currentOwnedStatus) { _isOwned = currentOwnedStatus; }
}