using UnityEngine;

public class ForeignObject : MonoBehaviour
{
    //so cringe
    [SerializeField] private BrainActionAgent _educationEpisode; //how do it for multiagent setting, 'cause it's only agent by one player

    [SerializeField] private float _foodValue;
    [SerializeField] private float _damageValue;
    //make parameters bellow formed by Transform scale and NavMesh moving
    [SerializeField] private float _size;
    [SerializeField] private bool _isMoving;

    [SerializeField] private bool _isOwned;
    [SerializeField] private float _objectHP;

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
        //Debug.Log("I'm died, my Food Value is: " + _foodValue);

        if ( _currentReceptor != null ) { _currentReceptor.ForeignObjectDestroy(this); }

        gameObject.SetActive(false);
    }


    public bool IsMoving() { return _isMoving; }

    public float GetFoodValue() { return _foodValue; }
    public float GetDamageValue() { return _damageValue; }
    public float GetObjectSize() { return _size; }
    public bool GetOwnedStatus() { return _isOwned; }
    public float GetObjectHP() { return _objectHP; }

    public void SetOwnedStatus(bool currentOwnedStatus) { _isOwned = currentOwnedStatus; }
}