using UnityEngine;
using UnityEngine.Events;
using TMPro;
using Photon.Pun;

public class PlayerNameManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField inputField;
    public UnityEvent NewNameEvent;

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LocalPlayer.NickName = "Player" + Random.Range(0, 1000);
        NewNameEvent.Invoke();
        inputField.text = PhotonNetwork.LocalPlayer.NickName;
    }

    public void EnterName()
    {
        PhotonNetwork.LocalPlayer.NickName = inputField.text;
        NewNameEvent.Invoke();
    }
}
