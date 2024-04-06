using System.Collections.Generic;
using UnityEngine;

public class Memory : MonoBehaviour
{
    [SerializeField] private Subconscious _subconscious;

    private List<MemoryObject> _memoryObjects = new List<MemoryObject>();
    private List<MemoryObject> _goals = new List<MemoryObject>();
    private List<MemoryObject> _ownedObjects = new List<MemoryObject>();


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


    public MemoryObject GetMostWantedObject() 
    {
        Sort(_memoryObjects);
        if ( _memoryObjects.Count > 0 ) { return _memoryObjects[_memoryObjects.Count - 1]; }
        else { return null; }
    }


    public MemoryObject GetMostWantedGoal()
    {
        Sort(_goals);
        return _goals[_goals.Count - 1];
    }


    public void ClearLists()
    {
        _goals.Clear();
        _memoryObjects.Clear(); 
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
