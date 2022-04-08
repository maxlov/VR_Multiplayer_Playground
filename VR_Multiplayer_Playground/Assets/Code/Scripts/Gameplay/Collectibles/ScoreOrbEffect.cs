using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ScoreOrbEffect", menuName = "Gameplay/Collectible/ScoreOrbEffect")]
public class ScoreOrbEffect : PickupEffect
{
    [SerializeField] private TeamScoreReference score;
    [SerializeField] private FloatReference amount;
    [SerializeField] private UnityEvent scoreOrbEvent;

    public override void Apply()
    {
        score.Value += amount.Value;
    }
}
