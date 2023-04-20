using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionalModel : MonoBehaviour
{
    private float _hapinnes = 0f;


    public void UpdateHapinnes(float changeValue)
    {
        _hapinnes += changeValue;
    }
}
