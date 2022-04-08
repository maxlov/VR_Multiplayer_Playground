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
    public bool level;
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
        currentText = text;
        if (useFloat && input != null)
            currentText = input.Value.ToString(floatToStringParams);
        textUI.text = currentText;
    }
    public void UpdateUpgradeText()
    {
        if (input != null)
        {
            if (level)
                currentText = "Level - " + input.Value.ToString();
            else
            {
                currentText = "Cost - " + input.Value.ToString();
            }
            textUI.text = currentText;
        }
    }
}
