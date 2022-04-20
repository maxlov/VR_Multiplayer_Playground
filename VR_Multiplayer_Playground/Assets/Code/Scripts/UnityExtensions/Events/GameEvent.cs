using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

[CreateAssetMenu(fileName = "GameEvent", menuName = "UnityExtensions/GameEvent")]
public class GameEvent : ScriptableObject
{
    /// <summary>
    /// The list of listeners that this event will notify if it is raised.
    /// </summary>
    public List<GameEventListener> eventListeners =
        new List<GameEventListener>();

    public void Raise()
    {
        UnityEngine.Debug.Log(this.name + " raised");
        for (int i = eventListeners.Count - 1; i >= 0; i--)
            eventListeners[i].OnEventRaised();
    }

    public void RegisterListener(GameEventListener listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}
