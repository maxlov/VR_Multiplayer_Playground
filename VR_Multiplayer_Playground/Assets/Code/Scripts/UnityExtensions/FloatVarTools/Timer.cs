using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [Tooltip("Leave empty and use alternate if you don't need to keep track of value")]
    [SerializeField] private FloatVariable timer;
    [Tooltip("Used if timer is null, to instantiate a float variable. Otherwise ignored")]
    [SerializeField] private float alternateTimer = 10f;
    [SerializeField] private UnityEvent timerDone;

    public bool isTicking;
    public bool continuous;

    private void Awake()
    {
        if (timer != null)
            return;

        timer = ScriptableObject.CreateInstance<FloatVariable>();
        timer.defaultValue = alternateTimer;
        timer.SetValue(alternateTimer);
    }

    private void Update()
    {
        if (!isTicking)
            return;

        timer.ApplyChange(-Time.deltaTime);

        if (timer.Value <= 0)
        {
            timer.SetValue(0);
            timerDone.Invoke();

            if (continuous)
                ResetTimer();
            else
                isTicking = false;
        }
    }

    public void ResetTimer()
    {
        timer.SetValue(timer.defaultValue);
        isTicking = true;
    }

    public void StopTimer()
    {
        isTicking = false;
        timer.SetValue(timer.defaultValue);
    }
}
