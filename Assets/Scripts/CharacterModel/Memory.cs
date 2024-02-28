using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memory : MonoBehaviour
{
    private List<Goal> _goals = new List<Goal>();


    public void MemorizeObject(Goal memorizableObject)
    {
        _goals.Add(memorizableObject);
    }
}
