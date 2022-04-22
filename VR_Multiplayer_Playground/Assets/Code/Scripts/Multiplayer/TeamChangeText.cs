using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class TeamChangeText : MonoBehaviour
{
    // Text stuff
    [SerializeField] static TextMeshProUGUI spectatorText;
    [SerializeField] private TextMeshProUGUI redTeamText;
    [SerializeField] private TextMeshProUGUI blueTeamText;
    private static string namedSpectators = "";
    private string redTeam = "";
    private string blueTeam = "";

    public static void SpectateTeamSet()
    {
        // Get the player's names that are connected
        foreach(Player p in PhotonNetwork.PlayerList)
        {
            // Check if they are part of the spectators team
            if ((int)p.CustomProperties["Team"] == 0)
            {
                Debug.Log("In the if");
                // If they are and have a nickname add their name
                if(p.NickName != null)
                    namedSpectators += $"<br> {p.NickName}";
                else
                {
                    namedSpectators += " <br> doesn't exist";
                }
            }
        }
        // Set up the new text
        spectatorText.text += namedSpectators;
        Debug.Log("End of the Start");
    }

    public void TeamChanger()
    {
        // Get the person's team number
        string playerName = (string)PhotonNetwork.LocalPlayer.NickName;
        int playerTeam = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];

        Debug.Log($"{playerName}");

        // Add their name under their team
        if (playerTeam == 2)
        {
            blueTeam += $"<br> {playerName}";
        } else if (playerTeam == 1)
        {
            redTeam += $"<br> {playerName}";
        } else
        {
            namedSpectators += $"<br> {playerName}!";
        }

        // Update the TMPGUI with nickname
        blueTeamText.text += blueTeam;
        redTeamText.text += redTeam;
        spectatorText.text += namedSpectators;
    }
}
