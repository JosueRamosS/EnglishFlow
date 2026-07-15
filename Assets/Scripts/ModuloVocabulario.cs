using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Controla el módulo de Vocabulario (Requisito 4): emparejar palabras en inglés
/// con su definición. Arma una ronda con "cantidadPares" pares tomados del banco
/// del nivel actual (GameProgress.ObtenerNivel), los reparte en dos columnas
/// (palabras / definiciones) mezcladas por separado, y evalúa los intentos.
///
/// Al terminar la ronda guarda puntaje, intentos y dominio en GameProgress
/// (Requisito 8) y muestra un panel de resumen.
/// </summary>
public class ModuloVocabulario : MonoBehaviour
{
    [Header("Transiciones")]
    public ScreenFader fader;

    [Header("Configuración de la ronda")]
    [Tooltip("Cuántos pares palabra-definición entran en una ronda.")]
    public int cantidadPares = 6;

    [Header("Referencias de UI")]
    public Transform contenedorPalabras;
    public Transform contenedorDefiniciones;
    public TarjetaVocabulario prefabTarjeta;
    public TMP_Text textoProgreso;

    [Header("Panel de resumen (al terminar)")]
    public GameObject panelResumen;
    public TMP_Text textoResumen;

    List<ParVocabulario> paresActivos;
    TarjetaVocabulario palabraSeleccionada;
    TarjetaVocabulario definicionSeleccionada;

    int aciertos = 0;
    int errores = 0;
    int paresResueltos = 0;
    bool evaluando = false; // bloquea clics mientras se muestra el resultado de un intento

    void Start()
    {
        if (panelResumen != null) panelResumen.SetActive(false);
        ArmarRonda();
    }

    void ArmarRonda()
    {
        List<ParVocabulario> banco = new List<ParVocabulario>(BancoVocabulario.ListaSegunNivel());
        Mezclar(banco);

        int cantidad = Mathf.Min(cantidadPares, banco.Count);
        paresActivos = banco.GetRange(0, cantidad);

        // Dos órdenes de aparición independientes: uno para la columna de palabras
        // y otro para la de definiciones, así no quedan alineadas en la misma fila.
        List<int> ordenPalabras = OrdenMezclado(cantidad);
        List<int> ordenDefiniciones = OrdenMezclado(cantidad);

        foreach (int indice in ordenPalabras)
            CrearTarjeta(contenedorPalabras, paresActivos[indice].palabra, indice, esPalabra: true);

        foreach (int indice in ordenDefiniciones)
            CrearTarjeta(contenedorDefiniciones, paresActivos[indice].definicion, indice, esPalabra: false);

        ActualizarProgreso();
    }

    void CrearTarjeta(Transform contenedor, string contenido, int indicePar, bool esPalabra)
    {
        TarjetaVocabulario tarjeta = Instantiate(prefabTarjeta, contenedor);
        tarjeta.Inicializar(contenido, indicePar, esPalabra, SeleccionarTarjeta);
    }

    List<int> OrdenMezclado(int cantidad)
    {
        List<int> orden = new List<int>();
        for (int i = 0; i < cantidad; i++) orden.Add(i);
        Mezclar(orden);
        return orden;
    }

    void Mezclar<T>(List<T> lista)
    {
        for (int i = lista.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (lista[i], lista[j]) = (lista[j], lista[i]);
        }
    }

    void SeleccionarTarjeta(TarjetaVocabulario tarjeta)
    {
        if (evaluando) return;

        if (tarjeta.esPalabra)
        {
            if (palabraSeleccionada == tarjeta) return;
            if (palabraSeleccionada != null) palabraSeleccionada.MostrarNormal();
            palabraSeleccionada = tarjeta;
        }
        else
        {
            if (definicionSeleccionada == tarjeta) return;
            if (definicionSeleccionada != null) definicionSeleccionada.MostrarNormal();
            definicionSeleccionada = tarjeta;
        }

        tarjeta.MostrarSeleccionada();

        if (palabraSeleccionada != null && definicionSeleccionada != null)
            StartCoroutine(EvaluarPar());
    }

    IEnumerator EvaluarPar()
    {
        evaluando = true;
        GameProgress.RegistrarIntento("vocabulario");

        TarjetaVocabulario palabra = palabraSeleccionada;
        TarjetaVocabulario definicion = definicionSeleccionada;
        bool correcto = palabra.indicePar == definicion.indicePar;

        if (correcto)
        {
            aciertos++;
            paresResueltos++;
            palabra.MostrarCorrecta();
            definicion.MostrarCorrecta();
            yield return new WaitForSeconds(0.3f);
            palabra.Bloquear();
            definicion.Bloquear();
        }
        else
        {
            errores++;
            palabra.MostrarIncorrecta();
            definicion.MostrarIncorrecta();
            yield return new WaitForSeconds(0.6f);
            palabra.MostrarNormal();
            definicion.MostrarNormal();
        }

        palabraSeleccionada = null;
        definicionSeleccionada = null;
        evaluando = false;
        ActualizarProgreso();

        if (paresResueltos >= paresActivos.Count)
            TerminarRonda();
    }

    void ActualizarProgreso()
    {
        if (textoProgreso != null)
            textoProgreso.text = $"Aciertos: {paresResueltos}/{paresActivos.Count}   Intentos: {aciertos + errores}";
    }

    void TerminarRonda()
    {
        int totalIntentos = aciertos + errores;
        int dominio = totalIntentos > 0 ? Mathf.RoundToInt(aciertos / (float)totalIntentos * 100f) : 0;
        int puntaje = aciertos * 10;

        GameProgress.GuardarPuntaje("vocabulario", puntaje);
        GameProgress.GuardarDominio("vocabulario", dominio);
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
        foreach (Transform hijo in contenedorPalabras) Destroy(hijo.gameObject);
        foreach (Transform hijo in contenedorDefiniciones) Destroy(hijo.gameObject);

        aciertos = 0;
        errores = 0;
        paresResueltos = 0;
        palabraSeleccionada = null;
        definicionSeleccionada = null;

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
