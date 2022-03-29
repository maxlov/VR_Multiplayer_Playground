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

    public bool useFloat;

    private void Start()
    {
        textUI = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        currentText = text;
        if (useFloat && input != null)
            currentText = input.Value.ToString();
        textUI.text = currentText;
    }
}
