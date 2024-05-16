using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class BrainActionAgent : Agent
{
    //for train episode initialize
    [SerializeField] private EventHandler _events;
    [SerializeField] private Receptors _receptors;
    [SerializeField] private PhysicalStatus _physicalStatus;
    [SerializeField] private Subconscious _subconscious;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private InstinctBrainAgent _instincts;
    [SerializeField] private EmotionalBrainAgent _emotions;
    [SerializeField] private BrainAgent _brainAgent;

    [SerializeField] private Brain _brain;

    private float _instinctDecision = 0f;
    private float _emotionalDecision = 0f;
    private float _finalDecision = 0f;
    private ICharacterAction _decidedAction;
    private GameObject _characher;
    private ForeignObject _currentForeignObject = null;


    private void Start()
    {
        _characher = transform.parent.gameObject;
    }


    //for a training
    private void Update()
    {
        CheckTrainEpisode();
    }


    public override void CollectObservations(VectorSensor sensor)
    {
        float objectNearStatus = 0f;
        if (_currentForeignObject != null && _physicalStatus.GetCurrentForeignObject() == _currentForeignObject) 
        { 
            objectNearStatus = 1f; 
        }

        float objectOwnetyStatus = 0f;
        if (_currentForeignObject != null && _currentForeignObject.IsOwned()) 
        { 
            objectOwnetyStatus = 1f; 
        }

        sensor.AddObservation(_instinctDecision);
        sensor.AddObservation(_emotionalDecision);
        sensor.AddObservation(_finalDecision);
        sensor.AddObservation(objectNearStatus);
        sensor.AddObservation(objectOwnetyStatus);
    }


    public override void OnActionReceived(ActionBuffers actions)
    {
        int decidedActionNumber = 0;
        for (int i = 0; i < actions.ContinuousActions.Length; i++) 
        {
            if (actions.ContinuousActions[decidedActionNumber] < actions.ContinuousActions[i])
            {
                decidedActionNumber = i;
            }
        }

        Debug.Log("For object " + _currentForeignObject + " i decided: ");
        switch (decidedActionNumber)
        {
            case 0:
                Debug.Log("Ignore");
                _decidedAction = null;
                break;
            case 1:
                Debug.Log("Walk");
                _decidedAction = new WalkAction(_characher);
                break;
            case 2:
                Debug.Log("Attack");
                _decidedAction = new AttackAction(_characher);
                break;
            case 3:
                Debug.Log("Heal");
                _decidedAction = new HealAction(_characher);
                break;
            case 4:
                Debug.Log("Mark");
                _decidedAction = new MarkAction(_characher);
                break;
            case 5:
                Debug.Log("Interact");
                _decidedAction = new InteractionAction(_characher);
                break;
        }

        _brain.IsFinalActionReady(true);
    }


    public override void OnEpisodeBegin()
    {
        _brain.ResetMemory();
        _brain.ResetSearchStatus();

        //_receptors.StopAllCoroutines();
        //_brain.StopAllCoroutines();

        _playerTransform.localPosition = Vector3.zero;
        _physicalStatus.SetRandomValues();

        _subconscious.WakeUp();

        _events.OnEpisodeReset.Invoke();
    }


    public void CheckTrainEpisode()
    {
        if (_physicalStatus.GetHealth() <= 0)
        {
            SetReward(-100f);
            _brainAgent.SetReward(-100f);
            _emotions.SetReward(-100f);
            _instincts.SetReward(-100f);
            EndEpisode();
        }

        if (_physicalStatus.GetHealth() == 100)
        {
            SetReward(50f);
            _brainAgent.SetReward(50f);
            _emotions.SetReward(50f);
            _instincts.SetReward(50f);
        }

        if (_physicalStatus.GetCurrentFoodResources() == 100)
        {
            SetReward(25f);
            _brainAgent.SetReward(25f);
            _emotions.SetReward(25f);
            _instincts.SetReward(25f);
        }

        if (_subconscious.GetHappines() == 0)
        {
            SetReward(100f);
            _brainAgent.SetReward(100f);
            _emotions.SetReward(100f);
            _instincts.SetReward(100f);
            EndEpisode();
        }
    }


    public void SetComplexReward(float reward)
    {
        SetReward(reward);
        _brainAgent.SetReward(reward);
        _emotions.SetReward(reward);
        _instincts.SetReward(reward);
    }


    public void SetInputs(float instinctDecision, float emotionalDecision, float finalDecision)
    {
        _instinctDecision = instinctDecision;
        _emotionalDecision = emotionalDecision;
        _finalDecision = finalDecision;
    }


    public void SetCurrentForeignObject(ForeignObject foreignObject) 
    {
        _currentForeignObject = foreignObject;
    }


    public ICharacterAction GetAction() { return _decidedAction; }
}