using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class AbstractNeed : MonoBehaviour
{
    protected float _severity = 0f; //По факту в том числе выполняет роль иерархии
    protected float _satisfaction = 0f; 

    
    protected void Initialize()
    {
        _severity = Random.value;
    }

    //Меняет степень выраженности после какого-то потрясения
    public void ChangeSeverity(float changeValue)
    {
        _severity += changeValue;
    }


    //Итоговое значение счастья для персонажа по данной потребности
    public float NeedResult()
    {
        //Формулу скорее всего нужно изменить, чтобы она правильно влияла на персонажа. Может быть усилить влияние выраженности.
        return  _satisfaction * _severity;
    }


    protected virtual void SatisfactionLevelCalculation() {}
}
