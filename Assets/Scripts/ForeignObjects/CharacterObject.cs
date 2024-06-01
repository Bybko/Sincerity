using System;
using UnityEngine;

public class CharacterObject : ForeignObject
{
    [SerializeField] private CharacterAgents _characterAgents;

    private PhysicalStatus _physicalStatus;
    private bool _isInsideStorage = false;


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
        _characterAgents.SetActionReward(-1f);
        _characterAgents.TotalEndEpisode();
        gameObject.SetActive(false);
        _events.OnForeignObjectDestroy.Invoke();
        _events.OnCharacterDestroy.Invoke();
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


    public override void SetStoredStatus(bool newStatus)
    {
        return;
    }


    public override bool IsStored()
    {
        return false;
    }


    public bool IsInside() { return _isInsideStorage; }

    public void SetInsideStatus(bool newStatus) { _isInsideStorage = newStatus; }
}
