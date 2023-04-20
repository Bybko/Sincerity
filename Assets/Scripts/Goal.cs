using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private float _satiety;


    public float Eating()
    {
        return _satiety;
    }
}
