using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatVariableMultiplier : MonoBehaviour
{
    [SerializeField] private FloatVariable[] floatVariables;
    [SerializeField] private FloatReference percentIncrease;

    public void Multiply()
    {
        foreach (var fv in floatVariables)
            fv.Value += fv.defaultValue * percentIncrease;
    }
}
