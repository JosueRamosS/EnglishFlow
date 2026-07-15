using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;

/// <summary>
/// Una opción de respuesta del módulo de Gramática (uno de los botones de opción
/// múltiple). Va sobre el mismo Prefab "Boton" que ya usás en el resto del juego
/// (con BotonNeobrutalista para el efecto de presionar/foco), y además tiñe su
/// "Relleno" de verde/rojo según la respuesta sea correcta o no.
///
/// No sabe nada del juego: solo avisa al ModuloGramatica cuando la tocan.
/// </summary>
[RequireComponent(typeof(Button))]
public class OpcionGramatica : MonoBehaviour
{
    [Tooltip("Rectángulo interior que cambia de color (hijo 'Relleno').")]
    public Image relleno;

    [Tooltip("Texto de la opción (hijo dentro de 'Relleno').")]
    public TMP_Text texto;

    [HideInInspector] public int indiceOpcion;

    Button boton;
    Action<OpcionGramatica> alTocar;

    void Awake()
    {
        boton = GetComponent<Button>();
        boton.onClick.AddListener(() => alTocar?.Invoke(this));
    }

    /// <summary>Configura el contenido de la opción. Lo llama ModuloGramatica al crearla.</summary>
    public void Inicializar(string contenido, int indiceOpcion, Action<OpcionGramatica> alTocar)
    {
        this.indiceOpcion = indiceOpcion;
        this.alTocar = alTocar;
        if (texto != null) texto.text = contenido;
        MostrarNormal();
    }

    public void MostrarNormal()
    {
        if (relleno != null) relleno.DOColor(PaletaColores.Blanco, 0.15f);
    }

    public void MostrarCorrecta()
    {
        if (relleno != null) relleno.DOColor(PaletaColores.Exito, 0.15f);
    }

    public void MostrarIncorrecta()
    {
        if (relleno != null) relleno.DOColor(PaletaColores.Error, 0.15f);
    }

    /// <summary>Bloquea la opción para que no se pueda volver a tocar (pregunta ya resuelta).</summary>
    public void Bloquear()
    {
        boton.interactable = false;
    }
}
