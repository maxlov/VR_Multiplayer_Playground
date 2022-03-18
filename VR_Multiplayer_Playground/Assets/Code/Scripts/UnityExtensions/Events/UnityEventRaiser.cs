using UnityEngine;
using UnityEngine.Events;

public class UnityEventRaiser : MonoBehaviour
{
    public UnityEvent OnEnableEvent;

    public void OnEnable()
    {
        OnEnableEvent.Invoke();
    }
}
