using UnityEngine;
using UnityEngine.Events;

public class Collectible : MonoBehaviour
{
    [SerializeField] private FloatVariable collectibleValue;
    [SerializeField] private FloatVariable score;
    [SerializeField] private UnityEvent ScoreUpdateEvent;

    private void OnCollisionEnter(Collision collision)
    {
        UpdateScore();
    }

    private void OnTriggerEnter(Collider other)
    {
        UpdateScore();
    }

    private void UpdateScore()
    {
        score.ApplyChange(collectibleValue.Value);
        ScoreUpdateEvent.Invoke();
        //gameObject.SetActive(false);
    }
}
