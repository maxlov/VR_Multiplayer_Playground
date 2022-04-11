using UnityEngine;
using Photon.Pun;
using UnityEngine.Events;

[RequireComponent(typeof(Timer))]
public class ObjectiveZone : MonoBehaviour
{
    [SerializeField] private FloatVariable teamObj;
    [SerializeField] private FloatVariable teamScore;

    [SerializeField] private FloatReference numberToScore;
    [SerializeField] private FloatReference pointsOnScore;

    public UnityEvent updateObjectiveCountEvent;
    public UnityEvent updateTeamScoreEvent;

    private Timer timer;
    [SerializeField] private Transform resetObjPosition;

    private void Awake()
    {
        timer = GetComponent<Timer>();
    }

    private void Start()
    {
        updateObjectiveCountEvent.Invoke();
    }

    public void UpdateTeamScore()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        ResetObj();
        teamScore.ApplyChange(pointsOnScore.Value);
        updateTeamScoreEvent.Invoke();
    }

    private void ResetObj()
    {
        Collider[] objects = Physics.OverlapBox(transform.position, transform.localScale / 2);
        int count = 0;
        foreach (var obj in objects)
        {
            if (count >= numberToScore)
                break;
            if (obj.gameObject.CompareTag("Objective"))
            {
                count++;
                obj.transform.position = resetObjPosition.position;
            }
        }
    }

    private void UpdateTeamObj()
    {
        Collider[] objects = Physics.OverlapBox(transform.position, transform.localScale / 2);
        int count = 0;

        foreach (var obj in objects)
            if (obj.gameObject.CompareTag("Objective"))
                count++;

        if (count < numberToScore && timer.isTicking)
            timer.StopTimer();
        if (count >= numberToScore && !timer.isTicking)
            timer.ResetTimer();

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
