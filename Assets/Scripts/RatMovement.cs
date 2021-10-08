using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatMovement : MonoBehaviour
{

    //comment: strg + k, strg + c
    //uncomment: strg + k, strg + u
    public int velocity;

    public GameObject Plattform;

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
        if((velocity > 0 && transform.position.x > x_right )||(velocity < 0 && transform.position.x < x_left))
        {
            velocity = velocity * (-1);
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(velocity, 0);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject geht auch mit tag
        if (collision.gameObject.name == "Plattform")
        {
            Plattform = collision.gameObject;
            plattform_width = Plattform.GetComponent<SpriteRenderer>().bounds.size.x;
            plattform_x = Plattform.GetComponent<Transform>().position.x;

            setPath(plattform_x, plattform_width);

        }

        if (collision.gameObject.name == "Character")
        {
            velocity = velocity * (-1);
        }

       
    }

    // calculate the borders of movement of rat
    void setPath(float platf_x, float platf_width)
    {
        float rat_width = this.GetComponent<SpriteRenderer>().bounds.size.x;

        x_left = platf_x - (platf_width / 2)+ (rat_width/2);
        x_right = platf_x + (platf_width / 2) - (rat_width / 2);
    }


}
