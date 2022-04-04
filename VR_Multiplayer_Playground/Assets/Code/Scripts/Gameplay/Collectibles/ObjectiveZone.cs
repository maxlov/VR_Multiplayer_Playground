using UnityEngine;
using UnityEngine.Events;

public class ObjectiveZone : MonoBehaviour
{
    [SerializeField] private FloatVariable teamObj;
    [SerializeField] private FloatVariable teamScore;
    public UnityEvent updateObjectiveCountEvent;
    public UnityEvent updateTeamScoreEvent;

    private void Start()
    {
        updateObjectiveCountEvent.Invoke();
    }

    public void UpdateTeamScore()
    {
        if (teamObj.Value < 1)
            return;

        teamScore.ApplyChange(teamObj);
        updateTeamScoreEvent.Invoke();
    }

    private void UpdateTeamObj()
    {
        Collider[] objects = Physics.OverlapBox(transform.position, transform.localScale / 2);
        int count = 0;

        foreach (var obj in objects)
            if (obj.gameObject.CompareTag("Objective"))
                count++;
        teamObj.SetValue(count);

        updateObjectiveCountEvent.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Objective"))
            UpdateTeamObj();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Objective"))
            UpdateTeamObj();
    }
}
