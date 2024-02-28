using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Здесь будет субъективное восприятие реальности, а пока вот так
public class Receptors : MonoBehaviour
{
    [SerializeField] private Memory _memory;
    // Если я хочу принимать данные с объектов, то хотя бы можно сделать не прям настолько втупую))))
    [SerializeField] private List<Goal> _goals;

    //Снизу временный говнокодик, потом сделать нормальный поиск внешних объектов.
    //Объекты не будут передаваться сразу в память, сначала будет ивент, что рецепторы заметили объект, с которым можно
    //взаимодействовать, потом мозг вызовет оценку этого объекта и уже сам мозг после оценки решит, сохранять его или нет.
    private void Start()
    {
        for (int i = 0; i < _goals.Count; i++)
        {
            _memory.MemorizeObject(_goals[i]);
        }
    }
}
