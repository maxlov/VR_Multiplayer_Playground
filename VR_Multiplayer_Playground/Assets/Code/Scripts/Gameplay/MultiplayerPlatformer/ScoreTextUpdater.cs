using UnityEngine;
using TMPro;

public class ScoreTextUpdater : MonoBehaviour
{
    [SerializeField] private FloatVariable score;
    private TextMeshProUGUI scoreUI;

    private void Start()
    {
        scoreUI = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        scoreUI.text = "Score:\n" + score.value;
    }
}
