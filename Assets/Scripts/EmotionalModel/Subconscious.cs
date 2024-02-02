using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subconscious : MonoBehaviour
{
    [SerializeField] private List<AbstractNeed> _charactersNeeds;
    private float _hapinnes = 0f;

    public void UpdateHapinnes(float changeValue)
    {
        _hapinnes += changeValue;
    }
}
