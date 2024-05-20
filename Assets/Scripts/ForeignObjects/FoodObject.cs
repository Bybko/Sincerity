using UnityEngine;

public class FoodObject : ForeignObject
{
    private void Start()
    {
        _events.OnEpisodeReset += ObjectReset;
    }


    public override void ObjectReset()
    {
        gameObject.SetActive(true);
        _objectHP = 100f;
        _owner = null;
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
        _owner.GetComponent<Receptors>().ForeignObjectLegacy(this);
    }


    public override void SelfDestroy()
    {
        gameObject.SetActive(false);
        _events.OnForeignObjectDestroy.Invoke();
        _events.OnEpisodeEnd.Invoke();
    }
}
