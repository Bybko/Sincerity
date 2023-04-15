using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionalModel : MonoBehaviour
{
    public Action HungerDiedEvent;

    [SerializeField] private float _hp;

    private float _hapinnes;


    private void Update()
    {
        _hp -= 0.02f;

        if (_hp <= 0)
        {
            HungerDiedEvent?.Invoke();
        }
    }

    public float UpdateHapinnes(float changeValue)
    {
        _hp += changeValue;
        _hapinnes = _hp;
        return _hapinnes;
    }

    public void ResetHP()
    {
        _hp = 100f;
    }
}
