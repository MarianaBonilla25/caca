using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleLightFlicker : MonoBehaviour
{
    private Light candleLight;
    private float baseIntensity;
    private Color baseColor;

    [Header("Flicker Settings")]
    public float intensityVariation = 0.2f;
    public float flickerSpeed = 5f;
    public float colorVariation = 0.05f;

    void Start()
    {
        candleLight = GetComponent<Light>();
        baseIntensity = candleLight.intensity;
        baseColor = candleLight.color;
    }

    void Update()
    {
        float flicker = Mathf.PerlinNoise(Time.time * flickerSpeed, 0.0f);
        candleLight.intensity = baseIntensity + flicker * intensityVariation;

        float r = baseColor.r + Random.Range(-colorVariation, colorVariation);
        float g = baseColor.g + Random.Range(-colorVariation, colorVariation);
        float b = baseColor.b + Random.Range(-colorVariation, colorVariation);

        candleLight.color = new Color(r, g, b);
    }
}
