using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private int tooManyCoins;
    private GameObject player;
    private bool flyingCoin;
    private Vector2 vel;
    private AudioSource audioSource;
    private AudioClip coinSound;
    private Animator anim;

    private void Start()
    {
        player = GameObject.Find("Character");
        flyingCoin = false;
        vel = new Vector2(1,0);
        audioSource = GameObject.Find("SFX").transform.GetChild(1).GetComponent<AudioSource>();
        coinSound = audioSource.clip;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (player.GetComponent<PlayerStats>().coins > tooManyCoins)
        {
            ChangeColliderSize(120);
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
        if (collision.gameObject == player)
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
        audioSource.PlayOneShot(coinSound);
        player.GetComponent<PlayerStats>().coins++;
        Destroy(gameObject);
    }
}
