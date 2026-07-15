using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;

/// <summary>
/// Una tarjeta individual del módulo de Vocabulario (una palabra o una definición).
/// Visualmente es un rectángulo con borde negro (mismo lenguaje neobrutalista que
/// los botones), cuyo relleno cambia de color según el estado: normal, seleccionada,
/// correcta o incorrecta.
///
/// La tarjeta no sabe nada del juego: solo avisa al ModuloVocabulario cuando la
/// tocan, a través de la acción "alTocar".
/// </summary>
[RequireComponent(typeof(Button))]
public class TarjetaVocabulario : MonoBehaviour
{
    [Tooltip("Rectángulo interior que cambia de color (hijo 'Relleno').")]
    public Image relleno;

    [Tooltip("Texto de la tarjeta (hijo dentro de 'Relleno').")]
    public TMP_Text texto;

    [HideInInspector] public int indicePar;   // a qué par palabra-definición pertenece
    [HideInInspector] public bool esPalabra;  // true = muestra la palabra, false = la definición

    Button boton;
    Action<TarjetaVocabulario> alTocar;

    void Awake()
    {
        boton = GetComponent<Button>();
        boton.onClick.AddListener(() => alTocar?.Invoke(this));
    }

    /// <summary>Configura el contenido de la tarjeta. Lo llama ModuloVocabulario al crearla.</summary>
    public void Inicializar(string contenido, int indicePar, bool esPalabra, Action<TarjetaVocabulario> alTocar)
    {
        this.indicePar = indicePar;
        this.esPalabra = esPalabra;
        this.alTocar = alTocar;
        if (texto != null) texto.text = contenido;
        MostrarNormal();
    }

    public void MostrarNormal()
    {
        if (relleno != null) relleno.DOColor(PaletaColores.Blanco, 0.15f);
    }

    public void MostrarSeleccionada()
    {
        if (relleno != null) relleno.DOColor(PaletaColores.Resalte, 0.15f);
    }

    public void MostrarCorrecta()
    {
        if (relleno != null) relleno.DOColor(PaletaColores.Exito, 0.15f);
    }

    public void MostrarIncorrecta()
    {
        if (relleno != null) relleno.DOColor(PaletaColores.Error, 0.15f);
    }

    /// <summary>Bloquea la tarjeta para que no se pueda volver a tocar (par ya resuelto).</summary>
    public void Bloquear()
    {
        boton.interactable = false;
    }
}
