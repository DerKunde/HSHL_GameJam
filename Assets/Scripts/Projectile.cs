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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Character")
        {
            collision.gameObject.GetComponent<GetGameManager>().gameManager.GetDamage();
            Destroy(gameObject);

        }
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
