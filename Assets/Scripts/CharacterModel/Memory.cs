using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Memory : MonoBehaviour
{
    //Временное пиво элементарное представление
    private List<Goal> _goals = new List<Goal>();

    //Временное пиво элементарное представление
    public bool TryingToRemember(Goal foreignObject)
    {
        for (int i = 0; i < _goals.Count; i++)
        {
            if (_goals[i] == foreignObject)
            {
                return true;
            }
        }
        return false;
    }

    
    public void MemorizeObject(Goal foreignObject)
    {
        _goals.Add(foreignObject);
    }
}
