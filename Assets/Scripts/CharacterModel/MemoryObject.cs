using UnityEngine;

public class MemoryObject
{
    //add time operations and plans later, also a last known position
    //also here would implement goal's methods for memory
    private ForeignObject _objectImage = null;
    private ICharacterAction _action = null;
    private Vector3 _objectPosition;
    private float _objectValue = 0f;

    private float _emotionalDecision = 0f;
    private float _instinctDecision = 0f;
    private float _finalDecision = 0f;


    public bool CheckImageObject() { return _objectImage.gameObject.activeSelf; }
    public bool IsEqual(ForeignObject comparableObject) { return _objectImage == comparableObject; }

    public float GetEmotionalDecision() { return _emotionalDecision; }
    public float GetInstinctDecision() { return _instinctDecision; }
    public float GetFinalDecision() { return _finalDecision; }
    public Vector3 GetObjectPosition() { return _objectPosition; }
    public ForeignObject GetObjectImage() { return _objectImage; }
    public float GetObjectValue() { return _objectValue; }
    public ICharacterAction GetAction() { return _action; }

    public void SetEmotionalDecision(float decision) { _emotionalDecision = decision; }
    public void SetInstinctDecision(float decision) { _instinctDecision = decision; }
    public void SetFinalDecision(float decision) { _finalDecision = decision; }
    public void SetObjectValue(float value) { _objectValue = value; }

    public void SetForeignObject(ForeignObject foreignObject) 
    { 
        _objectImage = foreignObject;
        _objectPosition = _objectImage.transform.position;
    }

    public void SetAction(ICharacterAction action)
    {
        _action = action;
        if (action != null)
        {
            _action.ConnectWithObject(this);
        }
    }
}