using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class AbstractNeed : MonoBehaviour
{
    protected float _severity = 0f; //ѕо факту в том числе выполн€ет роль иерархии
    protected float _satisfaction = 0f;


    public abstract void SatisfactionLevelCalculation();


    public void Initialize()
    {
        _severity = Random.value;
    }

    //ћен€ет степень выраженности после какого-то потр€сени€
    // ѕока закомментированно, ибо это слишком грубый процесс вмешательства в систему. ѕроцесс изменени€ выраженности должен
    // происходить более гладко и внутри системы.
    /*public void ChangeSeverity(float changeValue)
    {
        _severity += changeValue;
    }*/


    //»тоговое значение счасть€ дл€ персонажа по данной потребности
    public float NeedResult()
    {
        //‘ормулу скорее всего нужно изменить, чтобы она правильно вли€ла на персонажа. ћожет быть усилить вли€ние выраженности.
        return  _satisfaction * (_severity * 100);
    }
}
