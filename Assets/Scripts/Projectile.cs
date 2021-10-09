using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float lifeTime;


    private void Start()
    {
        Invoke("destroyProjectile", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    void destroyProjectile()
    {
        //TODO: Destroy Effect
        Destroy(gameObject);
    }
}
