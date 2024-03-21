using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Здесь будет субъективное восприятие реальности, а пока вот так
public class Receptors : MonoBehaviour
{
    [SerializeField] private Brain _brain;
    [SerializeField] private Subconscious _subconscious;
    [SerializeField] private BrainAgent _brainAgent; //for training
    // Если я хочу принимать данные с объектов, то хотя бы можно сделать не прям настолько втупую))))
    [SerializeField] private List<ForeignObject> _foreignObjects;


    private void Start()
    {
        //CheckForeignObjects();
    }
    //Снизу временный говнокодик, потом сделать нормальный поиск внешних объектов.
    //Объекты не будут передаваться сразу в память, сначала будет ивент, что рецепторы заметили объект, с которым можно
    //взаимодействовать, потом мозг вызовет оценку этого объекта и уже сам мозг после оценки решит, сохранять его или нет.
    public IEnumerator CheckForeignObjects()
    {
        foreach (ForeignObject foreignObject in _foreignObjects)
        {
            yield return StartCoroutine(_brain.AnalizeForeignObjects(foreignObject));
        }
    }
    //Кстати их же надо будет чистить их списка потом, когда они будут выходить за рамки доступного

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ForeignObject>(out ForeignObject goal)) 
        {
            _subconscious.ForeignObjectsInfluence(goal);
            _brainAgent.CheckTrainEpisode();
        }
    }
}
