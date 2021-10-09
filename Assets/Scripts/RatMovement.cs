using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatMovement : MonoBehaviour
{

    //comment: strg + k, strg + c
    //uncomment: strg + k, strg + u
    public int velocity;
    private bool dead;

    public GameObject Plattform;
    public Rigidbody2D Character;

    private float plattform_width;
    private float plattform_x;

    private float x_left;
    private float x_right;
    void Start()
    {
        if (velocity == 0) velocity = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            if ((velocity > 0 && transform.position.x > x_right) || (velocity < 0 && transform.position.x < x_left))
            {
                velocity = velocity * (-1);
            }

            GetComponent<Rigidbody2D>().velocity = new Vector2(velocity, 0);

        }
        else
        {
            transform.Rotate(0, 0, 1);
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
   
        if (collision.gameObject.layer == 6)
        {
            Plattform = collision.gameObject;
            plattform_width = Plattform.GetComponent<SpriteRenderer>().bounds.size.x;
            plattform_x = Plattform.GetComponent<Transform>().position.x;

            setPath(plattform_x, plattform_width);


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
            }
            else
            {
                velocity = velocity * (-1);
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

    //private void disable()
    //{
    //    Destroy();
    //}


}
