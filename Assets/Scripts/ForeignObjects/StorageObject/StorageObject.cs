using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageObject : ForeignObject
{
    [SerializeField] private List<StorageCell> _cells = new List<StorageCell>();
    [SerializeField] private GameObject _enterPosition = null;
    [SerializeField] private GameObject _exitPosition = null;

    private CharacterObject _visitor = null;


    private void Start()
    {
        _events.OnEpisodeReset += ObjectReset;
    }


    public override void SelfDestroy()
    {
        foreach (StorageCell cell in _cells)
        {
            cell.DestroyStoredObject();
        }
        gameObject.SetActive(false);

        _events.OnEpisodeEnd.Invoke();
    }


    public override void ObjectReset()
    {
        gameObject.SetActive(true);
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
        if (_visitor.IsInside()) 
        { 
            _visitor.gameObject.transform.position = _exitPosition.transform.position;
            _visitor.SetInsideStatus(false);
        }
        else 
        { 
            _visitor.gameObject.transform.position = _enterPosition.transform.position;
            _visitor.SetInsideStatus(true);
        }

        _visitor = null;
    }


    public void StoreObject(ForeignObject storableObject)
    {
        foreach (StorageCell cell in _cells)
        {
            cell.CheckStoredObject();
            if (!cell.IsOccupied()) 
            {
                cell.Teleporter(storableObject);
                return;
            }
        }
    }

    
    public bool IsFree()
    {
        foreach (StorageCell cell in _cells)
        {
            cell.CheckStoredObject();
            if (!cell.IsOccupied()) { return true; }
        }
        return false;
    }


    public float GetStoredAward()
    {
        float generalAward = 0f;
        foreach (StorageCell cell in _cells)
        {
            if (cell.GetStoredObject() != null)
            {
                if (cell.GetStoredObject().GetFoodValue() > 50 || cell.GetStoredObject().GetDamageValue() > 50)
                {
                    generalAward += 1f;
                }
            }
        }

        return generalAward;
    }


    public void SetVisitor(CharacterObject newVisitor) { _visitor = newVisitor; }
}
