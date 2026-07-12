using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Pantalla de configuración (Requisito 2).
/// Nivel de inglés (B1/B2), idioma de instrucciones y volumen.
/// Todo se guarda con GameProgress (PlayerPrefs) y se aplica al instante.
/// </summary>
public class Configuracion : MonoBehaviour
{
    [Header("Transiciones")]
    public ScreenFader fader;

    [Header("Controles UI")]
    [Tooltip("Dropdown con opciones B1 y B2 (en ese orden).")]
    public TMP_Dropdown dropdownNivel;

    [Tooltip("Dropdown con idiomas: Español, English (en ese orden).")]
    public TMP_Dropdown dropdownIdioma;

    [Tooltip("Slider de volumen (rango 0 a 1).")]
    public Slider sliderVolumen;

    void Start()
    {
        // Cargar los valores guardados y reflejarlos en la UI.
        if (dropdownNivel != null)
            dropdownNivel.value = (GameProgress.ObtenerNivel() == "B2") ? 1 : 0;

        if (dropdownIdioma != null)
            dropdownIdioma.value = (GameProgress.ObtenerIdioma() == "en") ? 1 : 0;

        if (sliderVolumen != null)
        {
            sliderVolumen.value = GameProgress.ObtenerVolumen();
            AudioListener.volume = sliderVolumen.value;
        }
    }

    // ---- Conectar estos métodos a los eventos OnValueChanged de cada control ----

    public void CambiarNivel(int indice)
    {
        GameProgress.GuardarNivel(indice == 1 ? "B2" : "B1");
    }

    public void CambiarIdioma(int indice)
    {
        GameProgress.GuardarIdioma(indice == 1 ? "en" : "es");
    }

    public void CambiarVolumen(float valor)
    {
        GameProgress.GuardarVolumen(valor);
        AudioListener.volume = valor; // se aplica en tiempo real
    }

    // ---- Botón de volver ----

    public void Volver()
    {
        if (fader != null)
            fader.FadeOut(() => CargadorDeEscenas.CargarEscena("MenuPrincipal"));
        else
            CargadorDeEscenas.CargarEscena("MenuPrincipal");
    }
}
