using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    [SerializeField] private Memory _memory;

    //Пока кринжовая проверка на воспоминание, ибо память реализована элементарно от задуманной.
    //Да и в целом функция пока кринжовая
    public void AnalizeForeignObjects(Goal foreignObject)
    {
        if (_memory.TryingToRemember(foreignObject))
        {
            Debug.Log("I remember it!");
        }
        else
        {
            _memory.MemorizeObject(foreignObject);
        }

        //Здесь вызываем подсознание и метод подсчёта всего для нейронок
        //Потом закидываем полученное в нейронки
    }
}
