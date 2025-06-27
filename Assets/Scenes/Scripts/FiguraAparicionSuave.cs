using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiguraAparicionSuave : MonoBehaviour
{
    public Transform figura;              // Objeto a mover
    public Transform destino;             // Punto visible (por la ventana)
    public float duracion = 1.5f;         // Tiempo que tarda en moverse
    public bool activarUnaVez = true;

    private bool yaActivado = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!yaActivado && other.CompareTag("Player"))
        {
            StartCoroutine(MoverFigura());
            if (activarUnaVez) yaActivado = true;
        }
    }

    private System.Collections.IEnumerator MoverFigura()
    {
        Vector3 origen = figura.position;
        Quaternion rotacionInicial = figura.rotation;
        Quaternion rotacionFinal = destino.rotation;

        float tiempo = 0f;

        while (tiempo < duracion)
        {
            float t = tiempo / duracion;
            figura.position = Vector3.Lerp(origen, destino.position, t);
            figura.rotation = Quaternion.Slerp(rotacionInicial, rotacionFinal, t);
            tiempo += Time.deltaTime;
            yield return null;
        }

        figura.position = destino.position;
        figura.rotation = rotacionFinal;
    }
}