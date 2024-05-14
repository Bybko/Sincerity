using UnityEngine;

public class ForeignObject : MonoBehaviour
{
    //so cringe
    [SerializeField] private BrainActionAgent _educationEpisode; //how do it for multiagent setting, 'cause it's only agent by one player

    [SerializeField] private float _objectHP;
    [SerializeField] private float _foodValue;
    [SerializeField] private float _damageValue;
    [SerializeField] private bool _isHealable;
    //make parameters bellow formed by Transform scale and NavMesh moving
    [SerializeField] private float _size;
    [SerializeField] private bool _isMoving;

    private bool _isOwned;
    //private ForeignObject _owner;
    private Receptors _currentReceptor;


    //so cringe
    private void Start()
    {
        _educationEpisode.OnEpisodeReset += ObjectReset;
    }


    public void OnTriggerEnter(Collider other)
    {
        Receptors receptors = other.GetComponent<Receptors>();
        if (receptors != null && other.isTrigger)
        {
            _currentReceptor = receptors;
            StartCoroutine(receptors.AddForeignObject(this));
        }
    }


    public void OnTriggerExit(Collider other)
    {
        Receptors receptors = other.GetComponent<Receptors>();
        if (receptors != null && other.isTrigger)
        {
            _currentReceptor = null;
            StartCoroutine(receptors.DeleteForeignObject(this));
        }
    }


    public void ObjectReset()
    {
        gameObject.SetActive(true);
        _objectHP = 100f;
        _isOwned = false;
    }        


    public void ChangeHP(float hpValue) 
    {
        _objectHP = Mathf.Clamp(_objectHP + hpValue, 0f, 100f);

        if (_objectHP == 0f)
        {
            SelfDestroy();
        }
    }


    private void SelfDestroy()
    {
        if ( _currentReceptor != null ) 
        {
            Debug.Log("I'm died, my Food Value is: " + _foodValue);
            _currentReceptor.ForeignObjectDestroy(this);
            _educationEpisode.SetComplexReward(10f);
        }

        gameObject.SetActive(false);
    }


    /*public bool IsOwned() 
    {
        if (_owner != null) { return true; }
        return false;
    }*/
    public bool IsOwned()
    {
        return _isOwned;
    }

    public bool IsMoving() { return _isMoving; }
    public bool IsHealable() { return _isHealable; }

    public float GetFoodValue() { return _foodValue; }
    public float GetDamageValue() { return _damageValue; }
    public float GetObjectSize() { return _size; }
    public float GetObjectHP() { return _objectHP; }

    //public void SetObjectOwner(ForeignObject newOwner) { _owner = newOwner; }
    public void SetObjectOwner(bool newOwner) { _isOwned = newOwner; }
}