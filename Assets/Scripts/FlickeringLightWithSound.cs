using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLightWithSound : MonoBehaviour
{
    [Header("Referencias")]
    public Light lightSource;                      // La luz real
    public AudioSource audioSource;                // Fuente de sonido
    public AudioClip flickerSound;                 // Clip largo que suena mientras parpadea
    public Renderer lampVisualRenderer;            // Renderer del objeto físico de la lámpara

    [Header("Material Colors")]
    public Color colorOn = Color.yellow;           // Color cuando la lámpara está encendida
    public Color colorOff = Color.black;           // Color cuando está apagada

    [Header("Flicker Settings")]
    public float flickerIntervalMin = 0.05f;
    public float flickerIntervalMax = 0.2f;
    public float flickerDuration = 3f;

    [Header("Stable Light Settings")]
    public float stableLightDuration = 2f;

    private Material lampMaterial;

    void Start()
    {
        if (lightSource == null)
            lightSource = GetComponent<Light>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if (lampVisualRenderer != null)
            lampMaterial = lampVisualRenderer.materials[0]; // <-- ESTA LÍNEA

        StartCoroutine(FlickerLoop());
    }

    System.Collections.IEnumerator FlickerLoop()
    {
        while (true)
        {
            // Iniciar sonido solo una vez al inicio del parpadeo
            if (audioSource != null && flickerSound != null && !audioSource.isPlaying)
            {
                audioSource.clip = flickerSound;
                audioSource.loop = true;
                audioSource.Play();
            }

            // PARPADEO
            float flickerEndTime = Time.time + flickerDuration;
            while (Time.time < flickerEndTime)
            {
                bool isOn = !lightSource.enabled;
                lightSource.enabled = isOn;

                if (lampMaterial != null)
                    lampMaterial.color = isOn ? colorOn : colorOff;

                float waitTime = Random.Range(flickerIntervalMin, flickerIntervalMax);
                yield return new WaitForSeconds(waitTime);
            }

            // LUZ FIJA ENCENDIDA
            lightSource.enabled = true;

            if (lampMaterial != null)
                lampMaterial.color = colorOn;

            if (audioSource != null && audioSource.isPlaying)
                audioSource.Stop();

            yield return new WaitForSeconds(stableLightDuration);
        }
    }
}