using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Receptors : MonoBehaviour
{
    [SerializeField] private Brain _brain;
    [SerializeField] private Subconscious _subconscious;
    [SerializeField] private BrainAgent _brainAgent; //for training

    [SerializeField] private List<ForeignObject> _viewedForeignObjects = new List<ForeignObject>();


    public IEnumerator AddForeignObject(ForeignObject foreignObject)
    {
        bool equal = false;
        foreach (ForeignObject currentforeignObject in _viewedForeignObjects)
        {
            if (foreignObject == currentforeignObject) { equal = true; }
        }

        if (!equal)
        {
            Debug.Log("I analize object " + foreignObject.GetFoodValue());
            yield return StartCoroutine(_brain.AnalizeForeignObject(foreignObject));
            Debug.Log("And for object " + foreignObject.GetFoodValue() + " decision is " + _brainAgent.GetFinalDecision());

            _viewedForeignObjects.Add(foreignObject);
        }

        yield return null;
    }


    public IEnumerator DeleteForeignObject(ForeignObject foreignObject)
    {
        float delayDelete = 10f;
        yield return new WaitForSeconds(delayDelete);
        _viewedForeignObjects.Remove(foreignObject);
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ForeignObject>(out ForeignObject goal))
        {
            _subconscious.ForeignObjectsInfluence(goal);
            _brainAgent.CheckTrainEpisode();
        }
    }
}