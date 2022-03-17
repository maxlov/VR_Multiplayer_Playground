using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatVariableMultiplier : MonoBehaviour
{
    [SerializeField] private FloatVariable[] floatVariables;
    [Range(1.0f, 3.0f)]
    [SerializeField] private float multiplier;

    public void Multiply()
    {
        foreach (var fv in floatVariables)
            fv.value *= multiplier;
    }
}
