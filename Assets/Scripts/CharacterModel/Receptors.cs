using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.AI;

public class Receptors : MonoBehaviour
{
    [SerializeField] private EventHandler _events;
    [SerializeField] private Brain _brain;
    [SerializeField] private Subconscious _subconscious;
    [SerializeField] private BrainActionAgent _brainAgent; //for training

    private List<ForeignObject> _viewedForeignObjects = new List<ForeignObject>();
    private ForeignObject _currentInteractObject;
    private Queue<IEnumerator> _coroutinesQueue = new Queue<IEnumerator>();
    private bool _isAnalizing = false;


    private void Start()
    {
        _brain.OnActionRemove += Sender;
        _events.OnForeignObjectDestroy += ViewedForeignObjectBrush;
    }


    private void Update()
    {
        if (_coroutinesQueue.Count > 0 && !IsAnalized()) 
        {
            SetAnalizingStatus(true);
            IEnumerator coroutine = _coroutinesQueue.Dequeue();
            StartCoroutine(coroutine);
        }
    }


    public void AddCoroutine(IEnumerator coroutine)
    {
        _coroutinesQueue.Enqueue(coroutine);
    }


    public void ResetCoroutinesQueue()
    {
        _coroutinesQueue.Clear();
        SetAnalizingStatus(false);
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
        float delayDelete = 5f;
        yield return new WaitForSeconds(delayDelete);
        _viewedForeignObjects.Remove(foreignObject);
    }


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<FoodObject>(out FoodObject goal))
        {
            SetCurrentInteractObject(goal);

            _brain.TellAboutReachingObject(goal);
        }
        else if (collision.gameObject.TryGetComponent<StorageObject>(out StorageObject storageGoal))
        {
            SetCurrentInteractObject(storageGoal);

            _brain.TellAboutReachingObject(storageGoal);
        }
    }


    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<FoodObject>(out FoodObject goal))
        {
            SetCurrentInteractObject(goal);

            _brain.TellAboutReachingObject(goal);
        }
        else if (collision.gameObject.TryGetComponent<StorageObject>(out StorageObject storageGoal))
        {
            SetCurrentInteractObject(storageGoal);

            _brain.TellAboutReachingObject(storageGoal);
        }
    }


    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<FoodObject>(out FoodObject goal))
        {
            SetCurrentInteractObject(null);
        }
        else if (collision.gameObject.TryGetComponent<StorageObject>(out StorageObject storageGoal))
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
    }


    private void Sender()
    {
        AddCoroutine(SendAllViewedObjects());
    }


    private IEnumerator SendAllViewedObjects()
    {
        //copy list for training, bc agent always randomly moves. After training it works well without copying
        List<ForeignObject> copyList = new List<ForeignObject>();
        copyList.AddRange(_viewedForeignObjects);

        foreach (ForeignObject viewedForeignObject in copyList)
        {
            yield return StartCoroutine(_brain.AnalizeForeignObject(viewedForeignObject));
        }
        SetAnalizingStatus(false);
    }


    private void ViewedForeignObjectBrush()
    {
        ForeignObject deletableObject = null;
        foreach (ForeignObject viewedForeignObject in _viewedForeignObjects)
        {
            if (!viewedForeignObject.gameObject.activeSelf)
            {
                deletableObject = viewedForeignObject;
            }
        }
        _viewedForeignObjects.Remove(deletableObject);
    }


    public bool IsForeignObjectNearBy() 
    { 
        if (_currentInteractObject == null) { return  false; }
        return true;
    }

    public bool IsAnalized() { return _isAnalizing; }

    public bool IsCoroutinesQueueOver() { 
        if (_coroutinesQueue.Count == 0 && !IsAnalized()) { return true; } 
        return false; 
    }

    public void SetCurrentInteractObject(ForeignObject foreignObject) { _currentInteractObject = foreignObject; }
    public void SetAnalizingStatus(bool newStatus) { _isAnalizing = newStatus; }

    public ForeignObject GetCurrentInteractObject() { return _currentInteractObject; }
}