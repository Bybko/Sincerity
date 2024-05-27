using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine.TextCore.Text;

public class InstinctActionAgent : Agent
{
    [SerializeField] private GameObject _character;
    [SerializeField] private Brain _brain;
    [SerializeField] private PhysicalStatus _status;

    private ForeignObject _currentForeignObject = null;
    private ICharacterAction _decidedAction;


    public override void CollectObservations(VectorSensor sensor)
    {
        float isCharacter = 0f;
        float foreignCharacterDamage = 0f;
        if (_currentForeignObject is CharacterObject) 
        { 
            isCharacter = 1f;
            foreignCharacterDamage = _currentForeignObject.GetDamageValue();
        }


        sensor.AddObservation(isCharacter);
        sensor.AddObservation(foreignCharacterDamage);
        sensor.AddObservation(_status.GetHealth());
        sensor.AddObservation(_status.GetPotentialDamage());
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
                if (!(_currentForeignObject is CharacterObject)) { SetReward(1f); }
                else { SetReward(-1f); }
                break;
            case 1:
                _decidedAction = new EscapeAction(_character);
                if (_currentForeignObject is CharacterObject && 
                    _currentForeignObject.GetDamageValue() <= _status.GetPotentialDamage()) { SetReward(1f); }
                else { SetReward(-1f); }
                break;
        }

        _brain.IsInstinctsActionReady(true);
    }


    public void SetCurrentForeignObject(ForeignObject foreignObject)
    {
        _currentForeignObject = foreignObject;
    }

    public ICharacterAction GetAction() { return _decidedAction; }
}
