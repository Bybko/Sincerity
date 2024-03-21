using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryObject
{
    //add time operations and plans later, also a last known posiotion
    private ForeignObject _objectImage = null;
    private float _emotionalDecision = 0f;
    private float _instinctDecision = 0f;


    public float GetEmotionalDecision() {  return _emotionalDecision; }
    public float GetInstinctDecision() { return _instinctDecision; }
    public Transform GetObjectTransform() { return _objectImage.transform; }

    public void SetEmotionalDecision(float decision) { _emotionalDecision = decision; }
    public void SetInstinctDecision(float decision) { _instinctDecision = decision; }
    public void SetForeignObject(ForeignObject foreignObject) { _objectImage = foreignObject; }
}
