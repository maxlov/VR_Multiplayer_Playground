using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private FloatVariable _charge;

    private bool isCharging = false;

    private void OnEnable() 
    {
        _input.chargeEvent += OnCharge;
        _input.chargeCancelledEvent += OnChargeCancelled;
    }

    private void OnDisable()
    {
        _input.chargeEvent -= OnCharge;
        _input.chargeCancelledEvent -= OnChargeCancelled;
    }

    private void Update()
    {
        if (isCharging)
            _charge.Value += Time.deltaTime;
        else
            _charge.Value -= Time.deltaTime;

        _charge.Value = Mathf.Clamp(_charge.Value, 0, 10);
    }

    private void OnCharge() { isCharging = true; }

    private void OnChargeCancelled() { isCharging = false; }
}
