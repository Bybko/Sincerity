using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private float _foodValue;
    [SerializeField] private float _safetyValue;

    //Очевидно что потом надо будет поменять, как это принимается. А то не буду же я для всех параметров геттеры писать. Мб ивенты сделать.
    public float GetFoodValue()
    {
        return _foodValue;
    }


    public float GetSafetyValue()
    {
        return _safetyValue;
    }
}
