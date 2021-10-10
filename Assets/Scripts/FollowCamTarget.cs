using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class FollowCamTarget : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cam;
    private Transform followObj;
    private Transform thisTransform;

    private void Awake()
    {
        thisTransform = GetComponent<Transform>();
    }

    private void Start()
    {
        followObj = cam.m_Follow;
    }

    private void Update()
    {
        thisTransform.position = new Vector3(thisTransform.position.x, followObj.position.y, thisTransform.position.z);
    }
}
