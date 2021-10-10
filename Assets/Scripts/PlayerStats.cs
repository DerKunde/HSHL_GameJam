using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int coins = 0;
    public int life;

    public ScriptableObject currentStats;

    private void Start()
    {
       // coins = 0;
    }

    private void Update()
    {
        //Debug.Log("coins: " + coins);
    }

    private void reduceHealth()
    {

    }

}
