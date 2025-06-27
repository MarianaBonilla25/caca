using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Light targetLight;
    public float baseIntensity = 0.6f;
    public float flickerAmplitude = 0.3f;
    public float flickerSpeed = 2f;

    void Start()
    {
        if (targetLight == null)
            targetLight = GetComponent<Light>();
    }

    void Update()
    {
        float flicker = Mathf.Sin(Time.time * flickerSpeed) * flickerAmplitude;
        targetLight.intensity = baseIntensity + flicker;
    }
}