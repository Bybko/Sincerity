using System.Collections.Generic;
using UnityEngine;

public class Memory : MonoBehaviour
{
    [SerializeField] private EventHandler _events;
    [SerializeField] private Subconscious _subconscious;

    private List<MemoryObject> _memoryObjects = new List<MemoryObject>();
    private List<MemoryObject> _goals = new List<MemoryObject>(); //why i actually need this list if goal could be sorted memoryObjects list? DELETE or MAKE IT NORMAL TO USE
    private List<MemoryObject> _ownedObjects = new List<MemoryObject>();


    private void Start()
    {
        _events.OnForeignObjectDestroy += ListBrush;
    }


    public void MemorizeObject(ForeignObject foreignObject, float instinct, float emotional, float final)
    {
        MemoryObject memoryObject = new MemoryObject();
        memoryObject.SetForeignObject(foreignObject);
        memoryObject.SetInstinctDecision(instinct);
        memoryObject.SetEmotionalDecision(emotional);
        memoryObject.SetFinalDecision(final);
        memoryObject.SetObjectValue(_subconscious.ObjectValueCalculate(foreignObject));
        _memoryObjects.Add(memoryObject);
    }

    
    public void SetNewAction(ForeignObject foreignObject, ICharacterAction action)
    {
        foreach (MemoryObject rememberedObject in _memoryObjects)
        {
            if (rememberedObject.IsEqual(foreignObject)) { rememberedObject.SetAction(action); }
        }
    }


    public MemoryObject Remember(ForeignObject foreignObject)
    {
        foreach (MemoryObject rememberedObject in _memoryObjects)
        {
            if (rememberedObject.IsEqual(foreignObject)) { return rememberedObject; }
        }
        return null;
    }


    public void AddNewGoal(MemoryObject newGoal)
    {
        for (int i = 0; i < _goals.Count; i++)
        {
            if (_goals[i].IsEqual(newGoal.GetObjectImage()))
            {
                _goals[i] = newGoal; //beacuse changes decisions of reference object 
                return;
            }
        }

        _goals.Add(newGoal);
    }


    public MemoryObject GetMostWantedObjectWithAction()
    {
        Sort(_memoryObjects);
        if (_memoryObjects.Count > 0)
        {
            for (int i = _memoryObjects.Count - 1; i >= 0; i--)
            {
                if (_memoryObjects[i].GetAction() != null)
                {
                    return _memoryObjects[i];
                }
            }
        }
        return null;
    }


    public MemoryObject GetMostWantedGoal()
    {
        if (_goals.Count > 0) 
        {
            Sort(_goals);
            return _goals[_goals.Count - 1];
        }
        return null;
    }


    public void ClearLists()
    {
        _goals.Clear();
        _memoryObjects.Clear();
        _ownedObjects.Clear();
    }


    public float TotalOwnedObjectsValue()
    {
        float totalValue = 0f;
        if (_ownedObjects.Count > 0)
        {
            foreach (MemoryObject ownedObject in _ownedObjects)
            {
                totalValue += ownedObject.GetObjectValue();
            }

            return Mathf.Clamp01(totalValue / _ownedObjects.Count);
        }
        return totalValue;
    }


    public void OwnObject(MemoryObject newObject)
    {
        _ownedObjects.Add(newObject);
    }


    public void ReleaseObject(ForeignObject releasableObject)
    {
        MemoryObject release = null;
        foreach(MemoryObject ownedObject in _ownedObjects)
        {
            if (ownedObject.IsEqual(releasableObject)) { release = ownedObject; }
        }

        _ownedObjects.Remove(release);
    }


    public float OwnedObjectsHealthStatus()
    {
        float mostWoundedObjectHP = 1f;
        if (_ownedObjects != null)
        {
            foreach (MemoryObject ownedObject in _ownedObjects)
            {
                float currentObjectHP = Mathf.Clamp01(ownedObject.GetObjectImage().GetObjectHP() / 100f);
                if (currentObjectHP < mostWoundedObjectHP)
                {
                    mostWoundedObjectHP = currentObjectHP;
                }
            }

            return mostWoundedObjectHP;
        }
        return mostWoundedObjectHP;
    }


    public void ReachObject(ForeignObject goal)
    {
        foreach (MemoryObject rememberedObject in _memoryObjects)
        {
            if (rememberedObject.IsEqual(goal)) 
            { 
                if (rememberedObject.GetAction() is WalkAction)
                {
                    rememberedObject.GetAction().SelfDelete();
                }
            }
        }
    }


    public StorageObject FindFreeOwnedStorageObject()
    {
        foreach (MemoryObject ownedObject in _ownedObjects)
        {
            if (ownedObject.GetObjectImage() is StorageObject) 
            {
                StorageObject potentialStorageObject = (StorageObject)ownedObject.GetObjectImage();
                if (potentialStorageObject.IsFree()) { return potentialStorageObject; }
            }
        }
        return null;
    }


    private void ListBrush()
    {
        MemoryObject deletableObject = null;

        foreach (MemoryObject memoryObject in _memoryObjects)
        {
            if (! memoryObject.CheckImageObject()) { deletableObject = memoryObject; }
        }

        _memoryObjects.Remove(deletableObject);
        _goals.Remove(deletableObject);
        //objects from _ownedObjects are deleting by special methods or stayed untill character remove them by self reflection
    }


    private void Sort(List<MemoryObject> sortList)
    {
        for (int i = 0; i < sortList.Count - 1; i++)
        {
            for (int j = 0; j < sortList.Count - i - 1; j++)
            {
                if (sortList[j].GetFinalDecision() > sortList[j + 1].GetFinalDecision())
                {
                    MemoryObject temp = sortList[j];
                    sortList[j] = sortList[j + 1];
                    sortList[j + 1] = temp;
                }
            }
        }
    }
}