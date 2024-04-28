using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Receptors : MonoBehaviour
{
    [SerializeField] private Brain _brain;
    [SerializeField] private Subconscious _subconscious;
    [SerializeField] private BrainActionAgent _brainAgent; //for training

    private List<ForeignObject> _viewedForeignObjects = new List<ForeignObject>();                     


    private void Start()
    {
        _brain.OnActionRemove += Sender;
    }


    public IEnumerator AddForeignObject(ForeignObject foreignObject)
    {
        bool equal = false;
        foreach (ForeignObject currentforeignObject in _viewedForeignObjects)
        {
            if (foreignObject == currentforeignObject) { equal = true; }
        }

        if (!equal)
        {
            yield return StartCoroutine(_brain.AnalizeForeignObject(foreignObject));

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


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<ForeignObject>(out ForeignObject goal))
        {
            _brain.TellAboutReachingObject(goal);
            _subconscious.ForeignObjectsInfluence(goal);
            _brainAgent.CheckTrainEpisode();
        }
    }

    
    public float EnvironmentDanger()
    {
        float totalDanger = 0f;
        foreach (ForeignObject foreignObject in _viewedForeignObjects) 
        {
            totalDanger = Mathf.Clamp01(totalDanger + _subconscious.ForeignObjectDangerCalculate(foreignObject));
        }
        return totalDanger;
    }


    private void Sender()
    {
        StartCoroutine(SendAllViewedObjects());
    }


    private IEnumerator SendAllViewedObjects()
    {
        List<ForeignObject> copyList = new List<ForeignObject>();
        copyList.AddRange(_viewedForeignObjects);
        foreach (ForeignObject viewedForeignObject in copyList)
        {
            yield return StartCoroutine(_brain.AnalizeForeignObject(viewedForeignObject));
        }
    }
}