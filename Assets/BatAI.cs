using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BatAI : MonoBehaviour
{
    public Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    bool onTheWay = false;
    float distanceBatAndChar;

    Vector2 startpoint;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        target = GameObject.Find("Character").transform;

      //  InvokeRepeating("UpdatePath", 0f, .5f);
        
    }
    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
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

        if(onTheWay && !attack(target, this.transform))
        {
            reachedEndOfPath = true;
            onTheWay = false;
            Debug.Log("konnte fliehen");
            CancelInvoke("UpdatePath");
        }
        if (!onTheWay && attack(target, this.transform))
        {
            onTheWay = true;
            startpoint = transform.position;
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
    }

    bool attack(Transform character, Transform bat)
    {
        distanceBatAndChar = Vector2.Distance(character.position, bat.position);

        if (distanceBatAndChar <= 5) return true;
        else return false;
    }

    void flyBack()
    {

    }
}
