using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] FloatVariable score;

    void Start()
    {
        score.Value = 0;
    }
}
