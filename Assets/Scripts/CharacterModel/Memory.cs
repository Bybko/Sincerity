using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Memory : MonoBehaviour
{
    //ѕосмотреть в гармонии, надо ли создавать список через new в определении по умолчанию или нет. »справить во всех других случа€х, где € создавал если не надо.
    //¬ременное пиво элементарное представление
    private List<Goal> _goals = new List<Goal>();

    //¬ременное пиво элементарное представление
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
