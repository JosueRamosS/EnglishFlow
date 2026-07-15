using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Controla el módulo de Gramática (Requisito 5): completar oraciones eligiendo
/// la opción correcta entre 3. Arma una ronda con "cantidadPreguntas" preguntas
/// tomadas del banco del nivel actual (GameProgress.ObtenerNivel), las muestra
/// una por una, y evalúa cada respuesta.
///
/// Al terminar la ronda guarda puntaje, intentos y dominio en GameProgress
/// (Requisito 8) y muestra un panel de resumen. Misma estructura que ModuloVocabulario,
/// adaptada a preguntas de una sola respuesta en vez de emparejamiento.
/// </summary>
public class ModuloGramatica : MonoBehaviour
{
    [Header("Transiciones")]
    public ScreenFader fader;

    [Header("Configuración de la ronda")]
    [Tooltip("Cuántas preguntas entran en una ronda.")]
    public int cantidadPreguntas = 6;

    [Header("Referencias de UI")]
    public TMP_Text textoOracion;
    public Transform contenedorOpciones;
    public OpcionGramatica prefabOpcion;
    public TMP_Text textoProgreso;

    [Header("Panel de resumen (al terminar)")]
    public GameObject panelResumen;
    public TMP_Text textoResumen;

    List<PreguntaGramatica> preguntasActivas;
    int preguntaActual = 0;
    int aciertos = 0;
    int errores = 0;
    bool evaluando = false;

    void Start()
    {
        if (panelResumen != null) panelResumen.SetActive(false);
        ArmarRonda();
    }

    void ArmarRonda()
    {
        List<PreguntaGramatica> banco = new List<PreguntaGramatica>(BancoGramatica.ListaSegunNivel());
        Mezclar(banco);

        int cantidad = Mathf.Min(cantidadPreguntas, banco.Count);
        preguntasActivas = banco.GetRange(0, cantidad);

        preguntaActual = 0;
        aciertos = 0;
        errores = 0;

        MostrarPregunta();
    }

    void MostrarPregunta()
    {
        if (preguntaActual >= preguntasActivas.Count)
        {
            TerminarRonda();
            return;
        }

        PreguntaGramatica pregunta = preguntasActivas[preguntaActual];
        if (textoOracion != null) textoOracion.text = pregunta.oracion;

        foreach (Transform hijo in contenedorOpciones) Destroy(hijo.gameObject);

        List<int> orden = new List<int>();
        for (int i = 0; i < pregunta.opciones.Length; i++) orden.Add(i);
        Mezclar(orden);

        foreach (int indice in orden)
        {
            OpcionGramatica opcion = Instantiate(prefabOpcion, contenedorOpciones);
            opcion.Inicializar(pregunta.opciones[indice], indice, SeleccionarOpcion);
        }

        ActualizarProgreso();
    }

    void SeleccionarOpcion(OpcionGramatica opcion)
    {
        if (evaluando) return;
        StartCoroutine(EvaluarOpcion(opcion));
    }

    IEnumerator EvaluarOpcion(OpcionGramatica opcionElegida)
    {
        evaluando = true;
        GameProgress.RegistrarIntento("gramatica");

        PreguntaGramatica pregunta = preguntasActivas[preguntaActual];
        bool correcto = opcionElegida.indiceOpcion == pregunta.indiceCorrecta;

        // Bloqueamos todas las opciones y revelamos cuál era la correcta,
        // sin importar si el jugador acertó o no (feedback educativo).
        foreach (Transform hijo in contenedorOpciones)
        {
            OpcionGramatica opcion = hijo.GetComponent<OpcionGramatica>();
            opcion.Bloquear();
            if (opcion.indiceOpcion == pregunta.indiceCorrecta)
                opcion.MostrarCorrecta();
        }

        if (correcto) aciertos++;
        else
        {
            errores++;
            opcionElegida.MostrarIncorrecta();
        }

        yield return new WaitForSeconds(0.9f);

        preguntaActual++;
        evaluando = false;
        MostrarPregunta();
    }

    void ActualizarProgreso()
    {
        if (textoProgreso != null)
            textoProgreso.text = $"Pregunta {preguntaActual + 1}/{preguntasActivas.Count}   Aciertos: {aciertos}";
    }

    void Mezclar<T>(List<T> lista)
    {
        for (int i = lista.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (lista[i], lista[j]) = (lista[j], lista[i]);
        }
    }

    void TerminarRonda()
    {
        int totalIntentos = aciertos + errores;
        int dominio = totalIntentos > 0 ? Mathf.RoundToInt(aciertos / (float)totalIntentos * 100f) : 0;
        int puntaje = aciertos * 10;

        GameProgress.GuardarPuntaje("gramatica", puntaje);
        GameProgress.GuardarDominio("gramatica", dominio);
        GameProgress.ActualizarRacha();

        if (panelResumen != null)
        {
            panelResumen.SetActive(true);
            if (textoResumen != null)
                textoResumen.text = $"¡Completado!\nDominio: {dominio}%\nPuntaje: {puntaje}";
        }
    }

    /// <summary>Vuelve a armar una ronda nueva sin salir del módulo. Conectala a un botón "Reintentar".</summary>
    public void Reintentar()
    {
        if (panelResumen != null) panelResumen.SetActive(false);
        ArmarRonda();
    }

    /// <summary>Conectala al botón "Volver" del módulo.</summary>
    public void Volver()
    {
        if (fader != null)
            fader.FadeOut(() => CargadorDeEscenas.CargarEscena("MenuPrincipal"));
        else
            CargadorDeEscenas.CargarEscena("MenuPrincipal");
    }
}
