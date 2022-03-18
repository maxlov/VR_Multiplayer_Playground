using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private FloatVariable speed;
    [SerializeField] private FloatVariable speedInitial;

    [Header("Jumping")]
    [SerializeField] private FloatVariable jumpHeight;
    [SerializeField] private FloatVariable jumpHeightInitial;
    [SerializeField] private FloatVariable jumpTime;
    [SerializeField] private FloatVariable jumpTimeInitial;
    [SerializeField] private FloatVariable fallMultiplier;
    [SerializeField] private FloatVariable fallMultiplierInitial;

    void Start()
    {
        speed.Value = speedInitial.Value;
        jumpHeight.Value = jumpHeightInitial.Value;
        jumpTime.Value = jumpTimeInitial.Value;
        fallMultiplier.Value = fallMultiplierInitial.Value;
    }
}
