using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class HurtEffect : MonoBehaviourPun
{
    public SpriteRenderer Sprite;

    public enum EventCodes
    {
        ColorChange = 0
    }

    private  void OnEnable() 
    {
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
    }

    private void OnDisable() 
    {
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
    }

    public void OnEvent(EventData photonEvent)
    {

        byte eventCode = photonEvent.Code;
        object content = photonEvent.CustomData;

        EventCodes code = (EventCodes)eventCode;
        if (code == EventCodes.ColorChange)
        {
            object[] datas = content as object[];
            if(datas.Length == 4)
            {
                if((int)datas[0] == base.photonView.ViewID)
                    Sprite.color = new Color((float)datas[1], (float)datas[2], (float)datas[3]);
            }
        }                   
    }

    public void GotHit()
    {
        ChangeColor_RED();
        StartCoroutine("ChangeColorOverTime");
    }
    public void ResetToWhite()
    {
        ChangeColor_WHITE();
    }

    IEnumerator ChangeColorOverTime()
    {
        yield return new WaitForSeconds(0.2f);
        ChangeColor_WHITE();
    }

    private void ChangeColor_RED()
    {
        float r = 1f, g = 0f, b = 0f; //red color

        object[] datas = new object[]{ base.photonView.ViewID, r, g, b};

        RaiseEventOptions options = new RaiseEventOptions()
        {
            CachingOption = EventCaching.DoNotCache,
            Receivers = ReceiverGroup.All
        };

        SendOptions sendOptions = new SendOptions();
        sendOptions.Reliability = true;

        PhotonNetwork.RaiseEvent((byte)EventCodes.ColorChange, datas, options, sendOptions);
    }

    private void ChangeColor_WHITE()
    {
        float r = 1f, g = 1f, b = 1f; //white color

        object[] datas = new object[]{ base.photonView.ViewID, r, g, b};

        RaiseEventOptions options = new RaiseEventOptions()
        {
            CachingOption = EventCaching.DoNotCache,
            Receivers = ReceiverGroup.All
        };

        SendOptions sendOptions = new SendOptions();
        sendOptions.Reliability = true;

        PhotonNetwork.RaiseEvent((byte)EventCodes.ColorChange, datas, options, sendOptions);
    }

}
