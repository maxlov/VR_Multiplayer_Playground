using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class FloatVariableResetter : MonoBehaviour
{
    [SerializeField] FloatVariable[] variablesToReset;
    [SerializeField] GameEvent resetEvent;
    private GameEventListener gameEventListener;
    private PhotonView photonView;

    [HideInInspector]
    public const byte ResetVariablesEventCode = 1;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();

        gameEventListener = gameObject.AddComponent<GameEventListener>();
        resetEvent.RegisterListener(gameEventListener);
        gameEventListener.Response = new UnityEvent();
        gameEventListener.Response.AddListener(ResetVariables);
    }

    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += PhotonOnEventResetVariables;
    }

    private void OnDisable()
    {
        resetEvent.UnregisterListener(gameEventListener);
        PhotonNetwork.NetworkingClient.EventReceived -= PhotonOnEventResetVariables;
    }

    private void ResetVariables()
    {
        object[] content = new object[] { };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(ResetVariablesEventCode, content, raiseEventOptions, SendOptions.SendReliable);
    }

    private void PhotonOnEventResetVariables(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;
        if (eventCode != ResetVariablesEventCode)
            return;
        Debug.Log("Resetting variables");
        foreach (var fv in variablesToReset)
            fv.SetValue(fv.defaultValue);
    }
}
