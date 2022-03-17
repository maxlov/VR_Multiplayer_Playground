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
            _charge.value += Time.deltaTime;
        else
            _charge.value -= Time.deltaTime;

        _charge.value = Mathf.Clamp(_charge.value, 0, 10);
    }

    private void OnCharge() { isCharging = true; }

    private void OnChargeCancelled() { isCharging = false; }
}
