using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRat : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isRatPlatform = false;

    private Movement Character;
    private GameObject Rat;

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            Character = collision.gameObject.GetComponent<Movement>();

            if (Character.debuff_enemies_to_character)
            {
                Debug.Log("ratte komm!");
                if(Rat != null)
                {
                    Rat.SendMessage("attackCharacter", collision.gameObject.transform.position.x);
                }
            }
        }

        if (collision.gameObject.layer == 9)
        {
            Rat = collision.gameObject;
            isRatPlatform = true;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Character = collision.gameObject.GetComponent<Movement>();

        if (collision.gameObject.layer == 3 && isRatPlatform && Character.debuff_enemies_to_character)
        {
            if (Rat != null)
            {
                Rat.SendMessage("stopAttacking");
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
