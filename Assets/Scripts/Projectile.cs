using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float lifeTime;
    public ParticleSystem destroyEffect;


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
        Instantiate(destroyEffect, transform.position, transform.rotation);
        destroyEffect.Play();
        Destroy(this);
    }
}
