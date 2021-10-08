using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class LandingShake : MonoBehaviour
{
    public CinemachineVirtualCamera cam;
    private Rigidbody2D _rb;
    private float _verticalVel = 0;
    [SerializeField] private float impactThreshold = 50f;
    [SerializeField] private float amplitude = 6f;
    [SerializeField] private float gain = 0.06f;
    [SerializeField] private float duration = 1f;
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _verticalVel = _rb.velocity.y;
    }

    void Update()
    {
        var impact = (_verticalVel - _rb.velocity.y) * Time.deltaTime;
        if (impact >= impactThreshold)
            StartCoroutine(ScreenShake());
        if (Input.GetKeyDown("o"))
            StartCoroutine(ScreenShake());
    }

    IEnumerator ScreenShake()
    {
        CinemachineBasicMultiChannelPerlin perlin = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = amplitude;
        perlin.m_FrequencyGain = gain;
        float remaining = duration;
        while (!remaining.Equals(0f))
        {
            remaining = Mathf.Max(remaining - Time.deltaTime, 0f);
            perlin.m_AmplitudeGain = amplitude * (remaining / duration);
            yield return null;
        }
        perlin.m_FrequencyGain = 0;
    }
}
