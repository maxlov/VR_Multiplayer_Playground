using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FloatVariableMultiplier : MonoBehaviour
{
    [SerializeField] private FloatVariable[] floatVariables;
    [SerializeField] private FloatReference percentIncrease;
    [SerializeField] private FloatReference timePercentInc;
    [SerializeField] private TeamScoreReference teamScore;
    [SerializeField] private FloatVariable statLevel;
    [SerializeField] private FloatVariable statCost;
    [SerializeField] private UnityEvent ScoreUpdateEvent;

    public void Multiply()
    {
        foreach (var fv in floatVariables)
        {
            if (fv.name == "JumpTime")
            {
                fv.Value += fv.defaultValue * timePercentInc;
            }
            fv.Value += fv.defaultValue * percentIncrease;
        }
    }

    public void Upgrade()
    {
        if (statCost.Value > teamScore.Value)
            return;

        //Score Stuff
        teamScore.Value -= statCost.Value;
        Debug.Log($"{teamScore.Value}");
        ScoreUpdateEvent.Invoke();

        //Change the Stat
        Multiply();

        //Stat Stuff
        statCost.Value = (statLevel.Value *(statLevel.Value+1f))/2;
        statLevel.Value++;
    }
}
