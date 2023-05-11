using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public GameObject canvas;
    public GameObject sceneCam;

    public TMP_Text pingrate;
    public TMP_Text spawnTimer;
    public GameObject respawnUI;
    public GameObject LeaveScreen;

    public GameObject winScreen;
    public GameObject youDiedScreen;

    private float  TimeAmount = 5;
    private bool startRespawn;

    [HideInInspector]
    public GameObject LocalPlayer;
    

    public static GameManager instance = null;

    public GameObject feedbox;
    public GameObject feedText_prefab;
    public GameObject KillBox;
    public GameObject killText_prefab;

    public GameObject MobileInputs;
    
    void Awake()
    {
        instance =this;
        canvas.SetActive(true);
    }

    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleLeaveScreen();
        }
        if(startRespawn)
        {
            // respawn on
            StartRespawn();
        }
        pingrate.text = "NetworkPing : " + PhotonNetwork.GetPing();
    }
    public void StartRespawn()
    {
        TimeAmount -= Time.deltaTime;
        spawnTimer.text = "Respawn in:" + TimeAmount.ToString("F0");

        if(TimeAmount<= 0)
        {
            respawnUI.SetActive(false);
            startRespawn = false;
            PlyaerRelocation();
            LocalPlayer.GetComponent<Health>().EnableInputs();
            LocalPlayer.GetComponent<PhotonView>().RPC("Revive",RpcTarget.AllBuffered);
        }
    }

    
    //code for win Screen, but it's not working. idk why
    /*
    public void Win()
    {
        StartCoroutine(CheckWin());
    }

    IEnumerator CheckWin()
    {
        yield return new WaitForSeconds(2f);
        int players = GameObject.FindGameObjectsWithTag("Player").Length;
        print(players);
        if(players == 1)
        {
            winScreen.SetActive(true);
        }
    }
    public void YouDied()
    {
        youDiedScreen.SetActive(true);
    }
    */

    
    public void ToggleLeaveScreen()
    {
        if(LeaveScreen.activeSelf)
        {
            LeaveScreen.SetActive(false);
        }
        else 
        {
            LeaveScreen.SetActive(true);
        }
    }
    

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        GameObject go = Instantiate(feedText_prefab, new Vector2(0f,0f), Quaternion.identity);
        go.transform.SetParent(feedbox.transform);
        go.GetComponent<TMP_Text>().text = newPlayer.NickName + "has joined the game";
        Destroy(go, 3);
    }
    
    

    public override void OnPlayerLeftRoom(Photon.Realtime.Player newPlayer)
    {
        GameObject go = Instantiate(feedText_prefab, new Vector2(0f,0f), Quaternion.identity);
        go.transform.SetParent(feedbox.transform);
        go.GetComponent<TMP_Text>().text = newPlayer.NickName + "has left the game";
        Destroy(go, 3);
    }
    
  
    
    
    
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    }

    public void PlyaerRelocation()
    {
        float randomPosition = Random.Range(-5,5);
        LocalPlayer.transform.localPosition = new Vector2(randomPosition, 2);

    }
    
    
    public void EnableRespawn()
    {
        TimeAmount = 5;
        startRespawn = true;
        respawnUI.SetActive(true);
    }

    public void SpawnPlayer()
    {
        float randomValue = Random.Range(-5, 5);
        PhotonNetwork.Instantiate(playerPrefab.name, new Vector2 (playerPrefab.transform.position.x * randomValue, playerPrefab.transform.position.y), Quaternion.identity, 0);
        canvas.SetActive(false);
        sceneCam.SetActive(false);
        MobileInputs.SetActive(true);
    }
}
