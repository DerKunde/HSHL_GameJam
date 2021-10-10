using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
    public Transform target;
    public Transform shootPoint;

    public float range;
    private float timeBtwShots;
    public float startTimeBtwShots;

    public Rigidbody2D projectile;

    private PlaySound sound;

    private void Start()
    {
        sound = GameObject.Find("SFX").GetComponent<PlaySound>();
    }


    void Update()
    {
        if (checkForPlayerInRange())
        {

            if (timeBtwShots <= 0)
            {
                LaunchProjectile();

                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
    }

    bool checkForPlayerInRange()
    {
        float distToPlayer = Vector3.Distance(target.transform.position, this.transform.position);

        if(distToPlayer < range)
        {
            return true;
        }

        return false;
    }

    Vector2 calculateVelocity(Vector2 target, Vector2 origin, float time)
    {
        Vector2 distance = target - origin;
        float Sy = distance.y;
        float Sx = distance.magnitude;

        float Vx = Sx / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time;

        Vector2 result = distance.normalized;
        result *= Vx;
        result.y = Vy;

        return result;
    }

    void LaunchProjectile()
    {
        sound.PlayThrowSound();
        Vector2 Vo = calculateVelocity(target.position * 0.1f, transform.position, 1f);

        //transform.rotation = Quaternion.LookRotation(Vo);

        Rigidbody2D obj = Instantiate(projectile, shootPoint.position, Quaternion.identity);
        obj.velocity = Vo;
    }
}
