using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BatDynamic : MonoBehaviour
{
    public Transform target;

    [Header("Distance (5 = easy, 6 = normal, 7 = hard)")]
    public float attackableDistance = 5;

    [Header("Speed of Bat")]
    [Range(400f, 600f)]
    public float speed;

    public float nextWaypointDistance = 3f;

    public Transform batGFX;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    bool attacking = false;
    float distanceBatAndChar;

    private Vector3 startpoint;
 


    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Character").transform;

        startpoint = rb.transform.position;
        
    }
    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            if (attack(target, transform))
            {
                seeker.StartPath(rb.position, target.position, OnPathComplete);
            }
            else
            {
                seeker.StartPath(rb.position, startpoint, OnPathComplete);
            }
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(attacking && !attack(target, transform) && (Vector2.Distance(startpoint, transform.position) < 2))
        {
            Debug.Log("Invokes get cancelled");
            CancelInvoke("UpdatePath");
            attacking = false;
        }

        if (!attacking && attack(target, transform))
        {
            attacking = true;
            InvokeRepeating("UpdatePath", 0f, .5f);
        }

        if (path == null) return;

        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        } else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (rb.velocity.x >= 0.05f)
        {
            transform.localScale = new Vector3(-0.05f, 0.05f, 1);
        }
        else if (rb.velocity.x < -0.01f)
        {
            transform.localScale = new Vector3(0.05f, 0.05f, 1);
        }
    }

    bool attack(Transform character, Transform bat)
    {
        distanceBatAndChar = Vector2.Distance(character.position, bat.position);

        if (distanceBatAndChar <= 7) return true;
        else return false;
    }

}
