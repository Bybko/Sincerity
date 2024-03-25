using UnityEngine;

public class MemoryObject
{
    //add time operations and plans later, also a last known posiotion
    //also here would implement goal's methods for memory
    private ForeignObject _objectImage = null;
    private float _emotionalDecision = 0f;
    private float _instinctDecision = 0f;
    private float _finalDecision = 0f;


    public bool IsEqual(ForeignObject comparableObject) { return _objectImage == comparableObject; }

    public float GetEmotionalDecision() {  return _emotionalDecision; }
    public float GetInstinctDecision() { return _instinctDecision; }
    public float GetFinalDecision() { return _finalDecision; }
    public Transform GetObjectTransform() { return _objectImage.transform; }
    public ForeignObject GetObjectImage() { return _objectImage; }  

    public void SetEmotionalDecision(float decision) { _emotionalDecision = decision; }
    public void SetInstinctDecision(float decision) { _instinctDecision = decision; }
    public void SetFinalDecision(float decision) { _finalDecision = decision; }
    public void SetForeignObject(ForeignObject foreignObject) { _objectImage = foreignObject; }
}
