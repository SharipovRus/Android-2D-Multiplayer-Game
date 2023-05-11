using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using System;

public class TimeOut : MonoBehaviourPun
{
    private float idleTime = 10f;
    private float timer = 10;

    public GameObject TimeOutUI;
    public TMP_Text TimeOutUI_Text; 

    private bool TimeOver = false;

    void Update()
    {
        //if(photonView.IsMine)
        //{
        if(!TimeOver)
        
        {
            if(Input.anyKey)
            {
                idleTime = 10;
            }
            idleTime -= Time.deltaTime;

            if(idleTime <= 0)
            {
                playerNotMoving();
            }

            if(TimeOutUI.activeSelf)
            {
                timer -= Time.deltaTime;
                TimeOutUI_Text.text = "Disconnecting in :" + timer.ToString("F0");
                if(timer <= 0)
                {
                    TimeOver = true;
                }
                else if( timer > 0 && Input.anyKey)
                {
                    idleTime = 10;
                    timer = 10;
                    TimeOutUI.SetActive(false);
                }
            }
        }
        else
        {
        //Leave to main menu
            LeaveGame();
        }
        //}
    }
    

    void LeaveGame()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);

    }

    public void playerNotMoving()
    {
        TimeOutUI.SetActive(true);
    }
}
