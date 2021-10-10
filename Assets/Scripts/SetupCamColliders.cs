using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupCamColliders : MonoBehaviour
{
    [SerializeField] private BoxCollider leftCollider;
    [SerializeField] private BoxCollider rightCollider;

    [SerializeField] private float gameAreaWidth = 40f;

    private void Awake()
    {
        leftCollider.center = new Vector3(gameAreaWidth * -0.5f - leftCollider.size.x * 0.5f, 0, 0);
        rightCollider.center = new Vector3(gameAreaWidth * 0.5f + rightCollider.size.x * 0.5f, 0, 0);
        
    }
}
