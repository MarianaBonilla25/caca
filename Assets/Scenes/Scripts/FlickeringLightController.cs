using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLightController : MonoBehaviour
{
    [Header("Referencias")]
    public Light lightSource;
    public AudioSource audioSource;
    public AudioClip flickerSound;
    public Renderer lampVisualRenderer;

    [Header("Colores de material")]
    public Color colorOn = Color.yellow;
    public Color colorOff = Color.black;

    [Header("Tiempos")]
    public float flickerIntervalMin = 0.05f;
    public float flickerIntervalMax = 0.2f;
    public float flickerDuration = 3f;
    public float stableLightDuration = 2f;

    private Material lampMaterial;

    void Start()
    {
        if (lightSource == null)
            lightSource = GetComponent<Light>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if (lampVisualRenderer != null)
        {
            // ¡IMPORTANTE! Creamos una instancia para no usar el material compartido del modelo
            Material[] mats = lampVisualRenderer.materials;
            mats[0] = new Material(mats[0]); // copiamos el primero
            lampVisualRenderer.materials = mats;

            lampMaterial = lampVisualRenderer.materials[0]; // ya es único
        }

        StartCoroutine(FlickerLoop());
    }

    System.Collections.IEnumerator FlickerLoop()
    {
        while (true)
        {
            // Sonido continuo durante parpadeo
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
                    lampMaterial.SetColor("_Color", isOn ? colorOn : colorOff);

                float waitTime = Random.Range(flickerIntervalMin, flickerIntervalMax);
                yield return new WaitForSeconds(waitTime);
            }

            // LUZ ENCENDIDA Y FIJA
            lightSource.enabled = true;

            if (lampMaterial != null)
                lampMaterial.SetColor("_Color", colorOn);

            if (audioSource != null && audioSource.isPlaying)
                audioSource.Stop();

            yield return new WaitForSeconds(stableLightDuration);
        }
    }
}