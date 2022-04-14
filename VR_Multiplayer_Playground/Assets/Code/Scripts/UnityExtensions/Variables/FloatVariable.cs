using UnityEngine;

[CreateAssetMenu(fileName = "FloatVariable", menuName = "UnityExtensions/FloatVariable")]
public class FloatVariable : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif

    [Header("Initial Value")]
    public float defaultValue;

    [Header("Current Value")]
    [SerializeField]
    private float _value;

    public float Value
    {
        get { return _value; }
        set { _value = value;  }
    }

    private void OnEnable()
    {
        _value = defaultValue;
    }

    public void SetValue(float value)
    {
        Value = value;
    }

    public void SetValue(FloatVariable value)
    {
        Value = value.Value;
    }

    public void ApplyChange(float amount)
    {
        Value += amount;
    }

    public void ApplyChange(FloatVariable amount)
    {
        Value += amount.Value;
    }
}
