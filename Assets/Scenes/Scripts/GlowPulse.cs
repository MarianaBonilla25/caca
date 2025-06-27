using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowPulse : MonoBehaviour
{
    public Renderer keyRenderer;
    public Color glowColor = new Color(1f, 1f, 0.6f); // Amarillo pálido, más cálido
    public float pulseSpeed = 1.5f;
    public float minEmission = 0.2f;
    public float maxEmission = 0.5f;

    private Material keyMat;

    void Start()
    {
        if (keyRenderer == null)
            keyRenderer = GetComponent<Renderer>();

        keyMat = keyRenderer.material;
        keyMat.EnableKeyword("_EMISSION");
    }

    void Update()
    {
        float emission = Mathf.Lerp(minEmission, maxEmission, Mathf.PingPong(Time.time * pulseSpeed, 1));
        Color finalColor = glowColor * Mathf.LinearToGammaSpace(emission);
        keyMat.SetColor("_EmissionColor", finalColor);
    }
}