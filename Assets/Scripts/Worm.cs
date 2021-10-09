using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{
    bool g2g;
    Vector3 dir;
    BoxCollider2D collider;

    [SerializeField] GameObject[] walls;
    private void Awake()
    {
        g2g = true;
        dir.x = 1;
        dir.y = 0;
        collider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        foreach (GameObject w in walls)
        {
            if (collider.IsTouching(w.GetComponent<BoxCollider2D>()))
            {
                dir.x *= -1;
            }
        }
    }

    private void FixedUpdate()
    {
        if (g2g)
        {
            transform.position += dir * Time.fixedDeltaTime;
        }
    }
}
