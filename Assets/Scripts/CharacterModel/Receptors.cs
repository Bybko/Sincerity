using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Здесь будет субъективное восприятие реальности, а пока вот так
public class Receptors : MonoBehaviour
{
    [SerializeField] private Brain _brain;
    [SerializeField] private Subconscious _subconscious;
    [SerializeField] private BrainAgent _brainAgent; //for training
    [SerializeField] private EmotionalBrainAgent _emotions; //for debugging temporary
    [SerializeField] private InstinctBrainAgent _instinct; //for debugging temporary
    // Если я хочу принимать данные с объектов, то хотя бы можно сделать не прям настолько втупую))))
    [SerializeField] private List<Goal> _foreignObjects;


    private void Start()
    {
        //CheckForeignObjects();
    }
    //Снизу временный говнокодик, потом сделать нормальный поиск внешних объектов.
    //Объекты не будут передаваться сразу в память, сначала будет ивент, что рецепторы заметили объект, с которым можно
    //взаимодействовать, потом мозг вызовет оценку этого объекта и уже сам мозг после оценки решит, сохранять его или нет.
    public IEnumerator CheckForeignObjects()
    {
        int i = 0;
        foreach (Goal foreignObject in _foreignObjects)
        {
            Debug.Log("Send object " + i);
            yield return StartCoroutine(_brain.AnalizeForeignObjects(foreignObject));
            Debug.Log("Emotions about object " + i + " are " + _emotions.GetEmotionalDecision());
            //Debug.Log("Instincts about object " + i + " are " + _instinct.GetInstinctDecision());
            //Debug.Log("I would like object " + i + " for " + _brainAgent.GetFinalDecision());
            i++;
        }
    }
    //Кстати их же надо будет чистить их списка потом, когда они будут выходить за рамки доступного

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Goal>(out Goal goal)) 
        {
            _subconscious.ForeignObjectsInfluence(goal);
            _brainAgent.CheckTrainEpisode();
        }
    }
}
