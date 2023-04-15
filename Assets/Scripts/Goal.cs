using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private float _satiety;
    [SerializeField] private float _damage;
    [SerializeField] private float _damageChance;


    public float Eating()
    {
        float result = _satiety;
        float randomValue = Random.Range(0f, 1f);

        if (randomValue <= _damageChance)
        {
            result -= _damage;
        }

        return result;
    }
}
