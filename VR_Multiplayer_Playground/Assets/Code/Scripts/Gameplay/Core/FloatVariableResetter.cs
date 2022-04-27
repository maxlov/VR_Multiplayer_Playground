using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

public class FloatVariableResetter : MonoBehaviour
{
    [SerializeField] FloatVariable[] variablesToReset;
    [SerializeField] GameEvent resetEvent;
    private GameEventListener gameEventListener;
    private PhotonView photonView;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();

        gameEventListener = gameObject.AddComponent<GameEventListener>();
        resetEvent.RegisterListener(gameEventListener);
        gameEventListener.Response = new UnityEvent();
        gameEventListener.Response.AddListener(ResetVariables);
    }

    private void OnDisable()
    {
        resetEvent.UnregisterListener(gameEventListener);
    }

    private void ResetVariables()
    {
        photonView.RPC("RPC_ResetVariables", RpcTarget.All);
    }

    [PunRPC]
    private void RPC_ResetVariables()
    {
        foreach (var fv in variablesToReset)
            fv.SetValue(fv.defaultValue);
    }


}
