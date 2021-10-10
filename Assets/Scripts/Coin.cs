using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private int tooManyCoins;
    public GameObject player;
    private bool flyingCoin;
    private Vector2 vel;
    private PlaySound coinSound;
    private Animator anim;

    private void Start()
    {
        player = GameObject.Find("Character");
        flyingCoin = false;
        vel = new Vector2(1,0);
        //coinSound = GameObject.Find("SFX").GetComponent<PlaySound>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //if (player.GetComponent<PlayerStats>().coins > tooManyCoins)
        //{
        //    ChangeColliderSize(120);
        //}

        if (player.GetComponent<GetGameManager>().gameManager != null)
        {
            if (player.GetComponent<GetGameManager>().gameManager.negativeCoinEffected())
            {
                ChangeColliderSize(120);
            }
        }


        if (flyingCoin)
        {
            transform.position = Vector2.SmoothDamp(transform.position, player.transform.position, ref vel, .3f) ;
            if(Vector2.Distance(transform.position, player.transform.position) < .5f)
            {
                CollectCoin();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if (collision.gameObject == player)
        if (collision.gameObject.name == "Character")
        {

            anim.enabled = false;

            if(GetComponent<CircleCollider2D>().radius > 12.7f)
            {
                flyingCoin = true;
            }
            else
            {
                CollectCoin();
            }
        }
    }

    public void ChangeColliderSize(float size)
    {
        GetComponent<CircleCollider2D>().radius = size;
    }

    private void CollectCoin()
    {
        //coinSound.PlayCoinSound();
        //player.GetComponent<PlayerStats>().coins++;
        if (player.GetComponent<GetGameManager>().gameManager != null)
        {
            player.GetComponent<GetGameManager>().gameManager.getCoins(1);
            Destroy(gameObject);
        }
    }
}
