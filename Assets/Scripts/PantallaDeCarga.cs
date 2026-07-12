using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

/// <summary>
/// Va en la escena "Carga" (Requisito 3).
/// Lee el destino guardado en CargadorDeEscenas y hace la carga ASÍNCRONA real
/// con LoadSceneAsync, moviendo una barra de progreso según el avance real.
/// </summary>
public class PantallaDeCarga : MonoBehaviour
{
    [Tooltip("La barra de progreso (componente Slider).")]
    public Slider barraProgreso;

    [Tooltip("Texto que muestra el porcentaje, ej: 45%. (Opcional)")]
    public TMP_Text textoPorcentaje;

    void Start()
    {
        // Empezamos a cargar la escena destino en segundo plano.
        StartCoroutine(CargarAsync(CargadorDeEscenas.EscenaDestino));
    }

    IEnumerator CargarAsync(string nombreEscena)
    {
        // Pequeña espera para que se vea la pantalla de carga aunque cargue rápido.
        yield return new WaitForSeconds(0.3f);

        AsyncOperation operacion = SceneManager.LoadSceneAsync(nombreEscena);

        // No activamos la escena hasta que la barra llegue al 100%.
        operacion.allowSceneActivation = false;

        while (!operacion.isDone)
        {
            // Unity reporta progreso de 0 a 0.9, luego espera activación.
            // Lo normalizamos a 0-1 dividiendo entre 0.9.
            float progreso = Mathf.Clamp01(operacion.progress / 0.9f);

            if (barraProgreso != null)
                barraProgreso.value = progreso;

            if (textoPorcentaje != null)
                textoPorcentaje.text = Mathf.RoundToInt(progreso * 100f) + "%";

            // Cuando la carga terminó (0.9), activamos la escena.
            if (operacion.progress >= 0.9f)
            {
                if (barraProgreso != null) barraProgreso.value = 1f;
                if (textoPorcentaje != null) textoPorcentaje.text = "100%";
                yield return new WaitForSeconds(0.2f);
                operacion.allowSceneActivation = true;
            }

            yield return null; // esperar al siguiente frame
        }
    }
}
