using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Bullete : MonoBehaviourPun
{
    public bool MovingDirecrion;
    public float MoveSpeed = 10;

    public float DestroyTime = 2f;
    public float bulletDamage = 0.3f;

    public string killer_name;
    public GameObject localPlayerOBJ;

    
    void Start() 
    {
        if(photonView.IsMine)
        killer_name = localPlayerOBJ.GetComponent<Player>().MyName;
    }
    
    IEnumerator destroyBullet()
    {
        yield return new WaitForSeconds(DestroyTime);
        this.GetComponent<PhotonView>().RPC("Destroy", RpcTarget.AllBuffered);
    }

    
    void Update() 
    {
        if(!MovingDirecrion)
        {
            transform.Translate(Vector2.right * MoveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * MoveSpeed * Time.deltaTime);
        }
    }
    
    [PunRPC]
    public void ChangeDirection()
    {
        MovingDirecrion = true;
    }
    
    [PunRPC]
    void Destroy()
    {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision) 
    {
        if (!photonView.IsMine)
        {
            return;
        }

        PhotonView target = collision.gameObject.GetComponent<PhotonView>();
        
        if (target != null && (!target.IsMine || target.IsRoomView))
        {
            if(target.tag == "Player")
            {
                target.RPC("HealthUpdate", RpcTarget.AllBuffered, bulletDamage);
                target.GetComponent<HurtEffect>().GotHit();

                if(target.GetComponent<Health>().health <= 0)
                {
                    Photon.Realtime.Player GotKilled = target.Owner;
                    target.RPC("YouGotKilledBy", GotKilled,killer_name);
                    target.RPC("YouKilled", localPlayerOBJ.GetComponent<PhotonView>().Owner, target.Owner.NickName);
                }
            }
            this.GetComponent<PhotonView>().RPC("Destroy", RpcTarget.AllBuffered);
        }
    }
}
