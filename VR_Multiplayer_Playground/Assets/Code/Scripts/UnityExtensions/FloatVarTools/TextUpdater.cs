using UnityEngine;
using TMPro;


[RequireComponent(typeof(TextMeshProUGUI))]
public class TextUpdater : MonoBehaviour
{
    [SerializeField] private FloatReference input;
    private TextMeshProUGUI textUI;

    [TextArea]
    public string text = "";
    private string currentText;

    public bool continuousUpdate = true;
    public bool useFloat;
    public bool textAfter = false;
    public string floatToStringParams;

    private void Start()
    {
        textUI = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (!continuousUpdate)
            return;
        UpdateText();
    }

    public void UpdateText()
    {
        if (useFloat && input != null)
        {
            if (!textAfter)
            {
                currentText = text;
                currentText += input.Value.ToString(floatToStringParams);
            }
            else
            {
                currentText = input.Value.ToString(floatToStringParams) + text;
            }
        }
        textUI.text = currentText;
    }
}
