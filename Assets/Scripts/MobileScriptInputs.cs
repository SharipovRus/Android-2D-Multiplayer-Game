using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MobileScriptInputs  : MonoBehaviour
{
    
    public GameObject localPlayer;
    
    
    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        // find local player from all the players in the scene
        foreach (GameObject player in players)
        {
            if (player.GetComponent<PhotonView>().IsMine)
            {
                localPlayer = player;
                break;
            }
        }
    }

    
    public void On_RightMove()
    {
        localPlayer.GetComponent<Player>().On_RightMove();
    }
    public void On_LeftMove()
    {
        localPlayer.GetComponent<Player>().On_LeftMove(); 
    }
     public void On_PointerExit()
    {
        localPlayer.GetComponent<Player>().On_PointerExit();
    }

    public void Jump()
    {
        
        localPlayer.GetComponent<Player>().Jump();
    }

    public void ShootPressed()
    {
        localPlayer.GetComponent<Player>().shot();
    }

    public void ShotReleased()
    {
        localPlayer.GetComponent<Player>().ShootUp();
    }
}