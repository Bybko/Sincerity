using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using Unity.VisualScripting;

public class BrainActionAgent : Agent
{
    [SerializeField] private PhysicalStatus _physicalStatus;
    [SerializeField] private Brain _brain;
    [SerializeField] private Memory _memory;

    private float _finalDecision = 0f;
    private ICharacterAction _decidedAction;
    private GameObject _character;
    private ForeignObject _currentForeignObject = null;


    private void Start()
    {
        _character = transform.parent.gameObject;
    }


    public override void CollectObservations(VectorSensor sensor)
    {
        float objectNearStatus = 0f;
        if (_currentForeignObject != null && _physicalStatus.GetCurrentForeignObject() == _currentForeignObject) 
        { 
            objectNearStatus = 1f; 
        }

        float objectOwnetyStatus = 0f;
        if (_currentForeignObject != null && _currentForeignObject.IsOwned()) { objectOwnetyStatus = 1f; }

        float objectStoredStatus = 0f;
        if (_currentForeignObject != null && _currentForeignObject.IsStored()) { objectStoredStatus = 0; }

        float selfInsideStatus = 0f;
        if (_character.GetComponent<CharacterObject>().IsInside()) { selfInsideStatus = 1f; }

        float readyForStorage = 0f;
        if (_memory.FindFreeOwnedStorageObject() != null) { readyForStorage = 1f; }


        sensor.AddObservation(_finalDecision);
        sensor.AddObservation(objectNearStatus);
        sensor.AddObservation(objectOwnetyStatus);
        sensor.AddObservation(objectStoredStatus);
        sensor.AddObservation(selfInsideStatus);
        sensor.AddObservation(readyForStorage);
        sensor.AddObservation(_physicalStatus.GetCurrentFoodResources());
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

        switch (decidedActionNumber)
        {
            case 0:
                _decidedAction = null;
                if (_currentForeignObject != null && _currentForeignObject is StorageObject &&
                    _currentForeignObject.IsOwned()) { SetReward(0.4f); }
                SetReward(0.1f);
                break;
            case 1:
                _decidedAction = new WalkAction(_character);
                if (_finalDecision > 0) { SetReward(0.1f); } else { SetReward(-0.1f); }
                break;
            case 2:
                _decidedAction = new AttackAction(_character);
                if (_finalDecision < 0) { SetReward(0.1f); } else { SetReward(-0.1f); }
                break;
            case 3:
                _decidedAction = new HealAction(_character);
                if (_finalDecision > 0) { SetReward(0.1f); } else { SetReward(-0.1f); }
                break;
            case 4:
                _decidedAction = new MarkAction(_character);
                if (_finalDecision > 0) { SetReward(0.2f); } else { SetReward(-0.1f); }
                break;
            case 5:
                _decidedAction = new InteractionAction(_character);
                if (_finalDecision > 0) { SetReward(0.1f); } else { SetReward(-0.1f); }
                if (_physicalStatus.GetCurrentFoodResources() < 50f) { SetReward(0.4f); }
                break;
            case 6:
                _decidedAction = new StorageAction(_character);
                if (_finalDecision > 0) { SetReward(0.1f); } else { SetReward(-0.1f); }
                if (_physicalStatus.GetCurrentFoodResources() > 50f) { SetReward(0.4f); }
                break;
        }

        _brain.IsFinalActionReady(true);
    }


    public void SetInputs(float finalDecision)
    {
        _finalDecision = finalDecision;
    }


    public void SetCurrentForeignObject(ForeignObject foreignObject) 
    {
        _currentForeignObject = foreignObject;
    }


    public ICharacterAction GetAction() { return _decidedAction; }
}