using UnityEngine;

public abstract class ForeignObject : MonoBehaviour
{
    [SerializeField] protected EventHandler _events;
    [SerializeField] protected float _foodValue;
    [SerializeField] protected float _damageValue;
    [SerializeField] protected float _objectHP;

    //make parameters bellow formed by Transform scale and NavMesh moving
    [SerializeField] protected float _size;
    [SerializeField] protected bool _isMoving;

    protected ForeignObject _owner;
    protected bool _isStored = false;
    

    public abstract void Interact();
    public abstract void ObjectReset();
    public abstract void SelfDestroy();
    public abstract void ChangeHP(float hpValue);


    public void OnTriggerEnter(Collider other)
    {
        Receptors receptors = other.GetComponent<Receptors>();
        if (receptors != null && other.isTrigger)
        {
            receptors.AddCoroutine(receptors.AddForeignObject(this));
        }
    }


    public void OnTriggerExit(Collider other)
    {
        Receptors receptors = other.GetComponent<Receptors>();
        if (receptors != null && other.isTrigger)
        {
            StartCoroutine(receptors.DeleteForeignObject(this)); //there is no need add this coroutine in queue, bc it
                                                                 //has an execution delay and will slow down the queue 
        }
    }

    public virtual bool IsOwned()
    {
        if (_owner != null) { return true; }
        return false;
    }
    public bool IsMoving() { return _isMoving; }
    public virtual bool IsStored() { return _isStored; }

    public float GetFoodValue() { return _foodValue; }
    public float GetDamageValue() { return _damageValue; }
    public float GetObjectHP() { return _objectHP; }
    public float GetObjectSize() { return _size; }
    public virtual ForeignObject GetOwner() { return _owner; }

    public virtual void SetObjectOwner(ForeignObject newOwner) { _owner = newOwner; }
    public virtual void SetStoredStatus(bool newStatus) { _isStored = newStatus; }
}