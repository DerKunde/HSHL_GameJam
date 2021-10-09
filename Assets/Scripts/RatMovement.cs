using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatMovement : MonoBehaviour
{

    //comment: strg + k, strg + c
    //uncomment: strg + k, strg + u
    public int velocity;
    private bool dead;
    private bool attacking = false;

    public GameObject Plattform;
    public Rigidbody2D Character;

    private float plattform_width;
    private float plattform_x;

    private float x_left;
    private float x_right;

    private float directionToChar;
    void Start()
    {
        if (velocity == 0) velocity = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead && !attacking)
        {
            if ((velocity > 0 && transform.position.x > x_right) || (velocity < 0 && transform.position.x < x_left))
            {
                velocity = velocity * (-1);
            }

            GetComponent<Rigidbody2D>().velocity = new Vector2(velocity, 0);

        }
        else if(dead)
        {
            transform.Rotate(0, 0, 1);
        }
        else if (attacking)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(velocity, 0);
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
   
        if (collision.gameObject.layer == 6) //plattform
        {
            Plattform = collision.gameObject;
            plattform_width = Plattform.GetComponent<SpriteRenderer>().bounds.size.x;
            plattform_x = Plattform.GetComponent<Transform>().position.x;

            setPath(plattform_x, plattform_width);

            Plattform.GetComponent<PlatformRat>().isRatPlatform = true;

        }

        if (collision.gameObject.name == "Character")
        {
            Character = collision.gameObject.GetComponent<Rigidbody2D>();
            if (Character.velocity.y < 0)
            {
                dead = true;
                GetComponent<Collider2D>().enabled = false;
                GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -5f);
                Character.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                Destroy(gameObject, 1);
                if(Plattform != null)
                {
                    Plattform.GetComponent<PlatformRat>().isRatPlatform = false;
                    Debug.Log("no longer rat platform");
                }
            }
            else
            {
                collision.gameObject.GetComponent<GetGameManager>().gameManager.GetDamage();
                velocity = velocity * (-1);
                attacking = false;
            }
        }
       
    }

    // calculate the borders of movement of rat
    void setPath(float platf_x, float platf_width)
    {
        float rat_width = this.GetComponent<SpriteRenderer>().bounds.size.x;

        x_left = platf_x - (platf_width / 2)+ (rat_width/2);
        x_right = platf_x + (platf_width / 2) - (rat_width / 2);
    }

    void attackCharacter(float character_x)
    {
        attacking = true;
        directionToChar = character_x - transform.position.x;
        Debug.Log("direction: " + directionToChar);
        if(directionToChar > 0)
        {
            velocity = 2;
        }else
        {
            velocity = -2;
        }
    }
    void stopAttacking()
    {
        attacking = false;
    }



}
