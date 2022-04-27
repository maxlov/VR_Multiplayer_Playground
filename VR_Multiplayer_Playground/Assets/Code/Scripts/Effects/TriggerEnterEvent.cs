using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEnterEvent : MonoBehaviour
{
    public UnityEvent ColliderEnteredEvent;

    private void OnTriggerEnter(Collider other)
    {
        ColliderEnteredEvent.Invoke();
    }
}
