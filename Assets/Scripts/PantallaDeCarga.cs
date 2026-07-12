using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

/// <summary>
/// Va en la escena "Carga" (Requisito 3).
/// Hace carga ASÍNCRONA real con LoadSceneAsync, PERO además fuerza un tiempo
/// mínimo visible (ej. 2 segundos) para que la barra se vea avanzar de 0 a 100%
/// contando los números, aunque la escena real cargue al instante.
/// </summary>
public class PantallaDeCarga : MonoBehaviour
{
    [Tooltip("La barra de progreso (componente Slider).")]
    public Slider barraProgreso;

    [Tooltip("Texto que muestra el porcentaje, ej: 45%. (Opcional)")]
    public TMP_Text textoPorcentaje;

    [Tooltip("Segundos que como mínimo durará la pantalla de carga.")]
    public float duracionMinima = 2f;

    void Start()
    {
        StartCoroutine(CargarAsync(CargadorDeEscenas.EscenaDestino));
    }

    IEnumerator CargarAsync(string nombreEscena)
    {
        AsyncOperation operacion = SceneManager.LoadSceneAsync(nombreEscena);

        // No activamos la escena hasta que terminemos la animación de la barra.
        operacion.allowSceneActivation = false;

        float tiempo = 0f;

        // Avanzamos la barra según el tiempo transcurrido (0 a 1) durante duracionMinima.
        while (tiempo < duracionMinima)
        {
            tiempo += Time.deltaTime;

            // progresoVisual va de 0 a 1 a lo largo de "duracionMinima".
            float progresoVisual = Mathf.Clamp01(tiempo / duracionMinima);

            if (barraProgreso != null)
                barraProgreso.value = progresoVisual;

            if (textoPorcentaje != null)
                textoPorcentaje.text = Mathf.RoundToInt(progresoVisual * 100f) + "%";

            yield return null; // esperar al siguiente frame
        }

        // Aseguramos que quede exactamente en 100%.
        if (barraProgreso != null) barraProgreso.value = 1f;
        if (textoPorcentaje != null) textoPorcentaje.text = "100%";

        // Pequeña pausa para que se vea el 100% antes de entrar.
        yield return new WaitForSeconds(0.2f);

        // La carga real ya terminó hace rato (progress >= 0.9); activamos la escena.
        operacion.allowSceneActivation = true;
    }
}