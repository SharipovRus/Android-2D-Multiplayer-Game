using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class Health : MonoBehaviourPun
{
    public Image fillImage;
    public float health = 1;
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public CapsuleCollider2D bc;
    public GameObject playerCanvas;

    public Player playerScript;
    public GameObject KillText;

    
    public void CheckHealth()
    {
        if(photonView.IsMine && health <= 0)
        {
            //respawn on
            GameManager.instance.EnableRespawn(); 
            playerScript.DisableInputs = true;
            this.GetComponent<PhotonView>().RPC("death", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    public void death()
    {
        rb.gravityScale = 0;
        bc.enabled = false;
        sr.enabled = false;
        playerCanvas.SetActive(false);
    }
    [PunRPC]
    public void Revive()
    {
        //disabled  win screen. can be turned on if want revive mechanic
        // but id doesnt work??? code below

        //this.gameObject.SetActive(false);
        
        rb.gravityScale = 1;
        bc.enabled = true;
        sr.enabled = true;
        playerCanvas.SetActive(true);
        fillImage.fillAmount = 1;
        health = 1;
        
    }

    public void EnableInputs()
    {
        playerScript.DisableInputs = false;
    }
    
    [PunRPC]
    public void HealthUpdate(float damage)
    {
        fillImage.fillAmount -= damage;
        health = fillImage.fillAmount;
        CheckHealth();
    }

    [PunRPC]
    void YouGotKilledBy(string name)
    {
        GameObject go = Instantiate(KillText, new Vector2(0,0), Quaternion.identity);
        go.transform.SetParent(GameManager.instance.KillBox.transform,false);
        go.GetComponent<Text>().text = "You Got Killed by : " + name;
        go.GetComponent<Text>().color = Color.black;
        //GameManager.instance.YouDied();
    }
    [PunRPC]
    void YouKilled(string name)
    {
        GameObject go = Instantiate(KillText, new Vector2(0,0), Quaternion.identity);
        go.transform.SetParent(GameManager.instance.KillBox.transform,false);
        go.GetComponent<Text>().text = "You Killed : " + name;
        go.GetComponent<Text>().color = Color.green;
        //GameManager.instance.Win();
    }
}
