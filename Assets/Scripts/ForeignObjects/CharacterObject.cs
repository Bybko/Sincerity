public class CharacterObject : ForeignObject
{
    private PhysicalStatus _physicalStatus;


    private void Start()
    {
        _owner = null;
        _physicalStatus = gameObject.GetComponent<PhysicalStatus>();
        _damageValue = _physicalStatus.GetPotentialDamage();

        _events.OnEpisodeReset += ObjectReset;
    }


    private void Update()
    {
        _objectHP = _physicalStatus.GetHealth();

        if (_objectHP == 0f) { SelfDestroy(); }
    }


    public override void Interact()
    {
        return;
    }


    public override void ObjectReset()
    {
        gameObject.SetActive(true);
    }


    public override void SelfDestroy()
    {
        //gameObject.SetActive(false);
        _events.OnForeignObjectDestroy.Invoke();
        _events.OnEpisodeEnd.Invoke();
    }


    public override void ChangeHP(float hpValue)
    {
        _physicalStatus.ChangeHealth(hpValue);
    }


    public override bool IsOwned()
    {
        return false;
    }


    public override ForeignObject GetOwner()
    {
        return null;
    }


    public override void SetObjectOwner(ForeignObject newOwner)
    {
        return;
    }
}
