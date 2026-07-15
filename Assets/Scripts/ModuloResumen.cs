using UnityEngine;
using TMPro;

/// <summary>
/// Controla la pantalla de Resumen (Requisito 10): progreso acumulado por módulo,
/// racha de días, y recomendación de qué módulo reforzar. Solo LEE datos de
/// GameProgress (no llama ActualizarRacha aquí — eso pasa al completar una ronda
/// en cada módulo, no por el simple hecho de mirar el resumen).
/// </summary>
public class ModuloResumen : MonoBehaviour
{
    [Header("Transiciones")]
    public ScreenFader fader;

    [Header("Datos por módulo (texto a llenar en cada fila)")]
    public TMP_Text datosVocabulario;
    public TMP_Text datosGramatica;
    public TMP_Text datosLectura;
    public TMP_Text datosEscritura;

    [Header("Racha y recomendación")]
    public TMP_Text textoRacha;
    public TMP_Text textoRecomendacion;
    public GameObject botonPracticarRecomendado;

    void Start()
    {
        MostrarDatosModulo("vocabulario", datosVocabulario);
        MostrarDatosModulo("gramatica", datosGramatica);
        MostrarDatosModulo("lectura", datosLectura);
        MostrarDatosModulo("escritura", datosEscritura);

        if (textoRacha != null)
            textoRacha.text = $"Racha actual: {GameProgress.ObtenerRacha()} día(s) seguidos";

        bool practicoAlgo = GameProgress.ObtenerIntentos("vocabulario") > 0
                          || GameProgress.ObtenerIntentos("gramatica") > 0
                          || GameProgress.ObtenerIntentos("lectura") > 0
                          || GameProgress.ObtenerIntentos("escritura") > 0;

        if (botonPracticarRecomendado != null) botonPracticarRecomendado.SetActive(practicoAlgo);

        if (textoRecomendacion != null)
        {
            textoRecomendacion.text = practicoAlgo
                ? $"Te recomendamos reforzar: {NombreLegible(GameProgress.ModuloARecomendar())}"
                : "Todavía no practicaste ningún módulo. ¡Elegí uno para empezar!";
        }
    }

    void MostrarDatosModulo(string modulo, TMP_Text destino)
    {
        if (destino == null) return;

        int intentos = GameProgress.ObtenerIntentos(modulo);
        if (intentos == 0)
        {
            destino.text = "Sin practicar todavía";
            return;
        }

        int dominio = GameProgress.ObtenerDominio(modulo);
        int puntaje = GameProgress.ObtenerPuntaje(modulo);
        destino.text = $"Dominio: {dominio}%   Puntaje: {puntaje}   Intentos: {intentos}";
    }

    string NombreLegible(string modulo)
    {
        switch (modulo)
        {
            case "vocabulario": return "Vocabulario";
            case "gramatica": return "Gramática";
            case "lectura": return "Lectura";
            case "escritura": return "Escritura";
            default: return modulo;
        }
    }

    string EscenaSegunModulo(string modulo)
    {
        switch (modulo)
        {
            case "gramatica": return "ModuloGramatica";
            case "lectura": return "ModuloLectura";
            case "escritura": return "ModuloEscritura";
            default: return "ModuloVocabulario";
        }
    }

    /// <summary>Conectala al botón "Practicar ahora" (lleva directo al módulo recomendado).</summary>
    public void PracticarRecomendado()
    {
        string escena = EscenaSegunModulo(GameProgress.ModuloARecomendar());
        if (fader != null)
            fader.FadeOut(() => CargadorDeEscenas.CargarEscena(escena));
        else
            CargadorDeEscenas.CargarEscena(escena);
    }

    /// <summary>Conectala al botón "Volver".</summary>
    public void Volver()
    {
        if (fader != null)
            fader.FadeOut(() => CargadorDeEscenas.CargarEscena("MenuPrincipal"));
        else
            CargadorDeEscenas.CargarEscena("MenuPrincipal");
    }
}
