using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class TeamChangeText : MonoBehaviour
{
    // Text stuff
    [SerializeField] private TextMeshProUGUI spectatorText;
    [SerializeField] private TextMeshProUGUI redTeamText;
    [SerializeField] private TextMeshProUGUI blueTeamText;
    private string namedSpectators = "";
    private string redTeam = "";
    private string blueTeam = "";

    public void TeamChanger()
    {
        // Update List of names for each team after join or button is pushed
        foreach(Player p in PhotonNetwork.PlayerList)
        {
            // Check if they are part of the spectators team
            if ((int)p.CustomProperties["Team"] == 0)
            {
                // If they are and have a nickname add their name
                if(p.NickName != null)
                    namedSpectators += $"<br> {p.NickName}!";
            }
            // Check if they are part of the red team
            else if ((int)p.CustomProperties["Team"] == 1)
            {
                // If they are and have a nickname add their name
                if(p.NickName != null)
                    redTeam += $"<br> {p.NickName}!";
            }
            // Check if they are part of the blue team
            else if ((int)p.CustomProperties["Team"] == 2)
            {
                // If they are and have a nickname add their name
                if(p.NickName != null)
                    blueTeam += $"<br> {p.NickName}!";
            }
        }
        // Update the TMPGUI with nickname
        blueTeamText.text = blueTeam;
        redTeamText.text = redTeam;
        spectatorText.text = namedSpectators;
    }
}
