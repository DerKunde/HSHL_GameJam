using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup(other);
        }
    }

    void Pickup(Collider2D player)
    {

        Debug.Log("picked up!");

        SpriteRenderer playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        playerSpriteRenderer.color = Color.green;

        Destroy(gameObject);
    }
}
