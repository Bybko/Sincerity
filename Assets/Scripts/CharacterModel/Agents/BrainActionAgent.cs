using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using Unity.VisualScripting;

public class BrainActionAgent : Agent
{
    [SerializeField] private PhysicalStatus _physicalStatus;
    [SerializeField] private Brain _brain;

    private float _finalDecision = 0f;
    private ICharacterAction _decidedAction;
    private GameObject _characher;
    private ForeignObject _currentForeignObject = null;


    private void Start()
    {
        _characher = transform.parent.gameObject;
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

        switch (decidedActionNumber)
        {
            case 0:
                _decidedAction = null;
                if (_finalDecision < 0) { SetReward(0.4f); } else { SetReward(-0.4f); }
                break;
            case 1:
                _decidedAction = new WalkAction(_characher);
                if (_finalDecision > 0) { SetReward(0.4f); } else { SetReward(-0.4f); }
                break;
            case 2:
                _decidedAction = new AttackAction(_characher);
                if (_finalDecision < 0) { SetReward(0.4f); } else { SetReward(-0.4f); }
                break;
            case 3:
                _decidedAction = new HealAction(_characher);
                if (_finalDecision > 0) { SetReward(0.4f); } else { SetReward(-0.4f); }
                break;
            case 4:
                _decidedAction = new MarkAction(_characher);
                if (_finalDecision > 0) { SetReward(0.4f); } else { SetReward(-0.4f); }
                break;
            case 5:
                _decidedAction = new InteractionAction(_characher);
                if (_finalDecision > 0) { SetReward(0.4f); } else { SetReward(-0.4f); }
                break;
            case 6:
                _decidedAction = new StorageAction(_characher);
                if (_finalDecision > 0) { SetReward(0.4f); } else { SetReward(-0.4f); }
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