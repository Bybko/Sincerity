using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Memory : MonoBehaviour
{
    private List<MemoryObject> _memoryObjects = new List<MemoryObject>();
    private List<MemoryObject> _goals = new List<MemoryObject>();
    //Временное пиво элементарное представление
    private List<ForeignObject> _goalss = new List<ForeignObject>();

    //Временное пиво элементарное представление
    public bool TryingToRemember(ForeignObject foreignObject)
    {
        for (int i = 0; i < _goalss.Count; i++)
        {
            if (_goalss[i] == foreignObject)
            {
                return true;
            }
        }
        return false;
    }

    
    public void MemorizeObject(ForeignObject foreignObject)
    {
        MemoryObject memoryObject = new MemoryObject();
        memoryObject.SetForeignObject(foreignObject);
        _memoryObjects.Add(memoryObject);
    }


    public void MemorizeDecision() { /*pass*/ }
}
