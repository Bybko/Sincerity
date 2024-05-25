using UnityEngine;

public class FoodObject : ForeignObject
{
    [SerializeField] private Vector3 _spawnPoint;

    private void Start()
    {
        _events.OnEpisodeReset += ObjectReset;
    }


    public override void ObjectReset()
    {
        gameObject.SetActive(true);
        gameObject.transform.localPosition = _spawnPoint;
        _objectHP = 100f;
        _owner = null;
        _isStored = false;
    }


    public override void ChangeHP(float hpValue)
    {
        if (hpValue < 0) 
        {
            _objectHP = Mathf.Clamp(_objectHP + hpValue, 0f, 100f);
        }

        if (_objectHP == 0f) 
        {
            SelfDestroy();
        }
    }


    public override void Interact()
    {
        if (_owner != null) 
        {
            _owner.GetComponent<Receptors>().ForeignObjectLegacy(this);
            _owner.GetComponentInChildren<Memory>().ReleaseObject(this);
            SelfDestroy();
        }
    }


    public override void SelfDestroy()
    {
        gameObject.SetActive(false);
        _events.OnForeignObjectDestroy.Invoke();
        //_events.OnEpisodeEnd.Invoke();
    }
}
