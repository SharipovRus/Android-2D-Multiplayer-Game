using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviourPun
{
    
    public float MoveSpeed = 5;
    public GameObject playerCam;
    public SpriteRenderer sprite;
    public PhotonView photonview;
    private bool AllowMoving = true; 
    public Animator anim;

    public GameObject BulletPrefab;
    public Transform BulletSpawnPointRight;
    public Transform BulletSpawnPointLeft;

    public TMP_Text PlayerName;
    public bool isGrounded = false;
    private Rigidbody2D rb;
    public float jumpForce;

    public bool DisableInputs = false;

    public string MyName;

    Vector3 Mobilemovement = Vector3.zero;
    


    void Awake()
    {
        if(photonView.IsMine)
        {
            GameManager.instance.LocalPlayer = this.gameObject;
            playerCam.SetActive(true);
            playerCam.transform.SetParent(null, false);
            PlayerName.text = "You: " + PhotonNetwork.NickName;
            PlayerName.color = Color.black;
            MyName = PhotonNetwork.NickName;;
        }
        else
        {
            PlayerName.text = photonview.Owner.NickName;
            PlayerName.color = Color.red;
        }
        
    }

    
    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(photonView.IsMine && !DisableInputs)
        {
            checkInputs();
        }
    }

    private void checkInputs()
    {
        if(AllowMoving)
        {
            if (false && Application.platform == RuntimePlatform.WindowsEditor)
            {
                var movement  = new Vector3(Input.GetAxisRaw("Horizontal"), 0);
                transform.position += movement * MoveSpeed * Time.deltaTime;
            }
            else
            {
                transform.position += Mobilemovement * MoveSpeed * Time.deltaTime;
            }
            
        }
        
        if(Input.GetKeyDown(KeyCode.RightControl) && anim.GetBool("IsMove") == false)
        {
            shot();
        }
        else if(Input.GetKeyUp(KeyCode.RightControl))
        {
            anim.SetBool("IsShot", false);
            AllowMoving = true;
        }

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.D) && anim.GetBool("IsShot") == false)
        {
            //FlipSprite_Right()
            anim.SetBool("IsMove", true);
            photonview.RPC("FlipSprite_Right", RpcTarget.AllBuffered);
        }
        else if(Input.GetKeyUp(KeyCode.D))
        {
            anim.SetBool("IsMove", false);
        }

        if(Input.GetKeyDown(KeyCode.A) && anim.GetBool("IsMove") == false)
        {
            anim.SetBool("IsMove", true);
            //FlipSprite_Left()
            photonview.RPC("FlipSprite_Left", RpcTarget.AllBuffered);
        }
        else if(Input.GetKeyUp(KeyCode.A))
        {
            anim.SetBool("IsMove", false);
        }

    }

    public void shot()
    {
        
        if(anim.GetBool("IsMove") == true)
        return;
        
        if(sprite.flipX == false)
        {
            GameObject bullete = PhotonNetwork.Instantiate(BulletPrefab.name, new Vector2(BulletSpawnPointRight.position.x, BulletSpawnPointRight.position.y), Quaternion.identity, 0);
            bullete.GetComponent<Bullete>().localPlayerOBJ = this.gameObject;
        }
        
        if(sprite.flipX == true)
        {
            GameObject bullete = PhotonNetwork.Instantiate(BulletPrefab.name, new Vector2(BulletSpawnPointLeft.position.x, BulletSpawnPointLeft.position.y), Quaternion.identity, 0);
            bullete.GetComponent<Bullete>().localPlayerOBJ = this.gameObject;
            bullete.GetComponent<PhotonView>().RPC("ChangeDirection", RpcTarget.AllBuffered);
        }
        anim.SetBool("IsShot", true);
        AllowMoving = false;

        
    }

    public void ShootUp()
    {
        anim.SetBool("IsShot", false);
        AllowMoving = true;
    }
    
    [PunRPC]

    private void FlipSprite_Right()
    {
        sprite.flipX = false;
    }

    [PunRPC]

    private void FlipSprite_Left()
    {
        sprite.flipX = true;
    }

    void OnCollisionEnter2D(Collision2D col) 
    {
        if(col.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D col) 
    {
        if(col.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    public void Jump()
    {
        if(isGrounded)
        rb.AddForce(new Vector2(0, jumpForce * Time.deltaTime)); 
    }

    #region MOBILE INPUTS

    public void On_RightMove()
    {
        anim.SetBool("IsMove", true);
        if(anim.GetBool("IsShot") == false)
        {
            photonview.RPC("FlipSprite_Left", RpcTarget.AllBuffered);
        }
        //sr.flipX = false;
        //pv.RPC("OnDirectionChange_RIGHT", RpcTarget.Others);
        photonview.RPC("FlipSprite_Right", RpcTarget.AllBuffered);
        Mobilemovement = new Vector3(1, 0, transform.position.z);
    }

    public void On_LeftMove()
    {
        anim.SetBool("IsMove", true);
        if(anim.GetBool("IsShot") == false)
        {
            photonview.RPC("FlipSprite_Left", RpcTarget.AllBuffered);
        }
        //sr.flipX = true;
        //pv.RPC("OnDirectionChange_LEFT", RpcTarget.Others);
        Mobilemovement = new Vector3(-1, 0, transform.position.z);
    }
    
    public void On_PointerExit()
    {
        anim.SetBool("IsMove", false);
        Mobilemovement = new Vector3(0, 0, transform.position.z);
    }



    #endregion
}
