using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.AI;

public class Receptors : MonoBehaviour
{
    [SerializeField] private Brain _brain;
    [SerializeField] private Subconscious _subconscious;
    [SerializeField] private BrainActionAgent _brainAgent; //for training

    private List<ForeignObject> _viewedForeignObjects = new List<ForeignObject>();
    private ForeignObject _currentInteractObject;
    private Queue<IEnumerator> coroutinesQueue = new Queue<IEnumerator>();
    private bool _isAnalizing = false;


    private void Start()
    {
        _brain.OnActionRemove += Sender;
    }


    private void Update()
    {
        if (coroutinesQueue.Count > 0 && !IsAnalized()) 
        {
            SetAnalizingStatus(true);
            IEnumerator coroutine = coroutinesQueue.Dequeue();
            StartCoroutine(coroutine);
        }
    }


    public void AddCoroutine(IEnumerator coroutine)
    {
        coroutinesQueue.Enqueue(coroutine);
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

        SetAnalizingStatus(false);
        yield return null;
    }


    public IEnumerator DeleteForeignObject(ForeignObject foreignObject)
    {
        float delayDelete = 10f;
        yield return new WaitForSeconds(delayDelete);
        _viewedForeignObjects.Remove(foreignObject);
        SetAnalizingStatus(false);
    }


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<FoodObject>(out FoodObject goal))
        {
            SetCurrentInteractObject(goal);

            _brain.TellAboutReachingObject(goal);
            _brainAgent.CheckTrainEpisode();
        }
    }


    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<FoodObject>(out FoodObject goal))
        {
            SetCurrentInteractObject(null);
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

   
    public void ForeignObjectLegacy(ForeignObject foreignObject)
    {
        _subconscious.ForeignObjectsInfluence(foreignObject);
        _brainAgent.CheckTrainEpisode();
    }


    private void Sender()
    {
        //StartCoroutine(SendAllViewedObjects());
        AddCoroutine(SendAllViewedObjects());
    }


    private IEnumerator SendAllViewedObjects()
    {
        //would be better if agent is stopping while this function complete instead copy that list
        List<ForeignObject> copyList = new List<ForeignObject>();
        copyList.AddRange(_viewedForeignObjects);
        foreach (ForeignObject viewedForeignObject in copyList)
        {
            yield return StartCoroutine(_brain.AnalizeForeignObject(viewedForeignObject));
        }
        SetAnalizingStatus(false);
    }


    public bool IsForeignObjectNearBy() 
    { 
        if (_currentInteractObject == null) { return  false; }
        return true;
    }
    public bool IsAnalized() { return _isAnalizing; }

    public void SetCurrentInteractObject(ForeignObject foreignObject) { _currentInteractObject = foreignObject; }
    public void SetAnalizingStatus(bool newStatus) { _isAnalizing = newStatus; }

    public ForeignObject GetCurrentInteractObject() { return _currentInteractObject; }
}