using UnityEngine;
using DG.Tweening;
using System;

/// <summary>
/// Maneja las transiciones fade in / fade out entre pantallas (Requisito 9).
/// Necesita un Panel negro que cubra toda la pantalla con un componente CanvasGroup.
/// Al entrar a una escena hace fade IN (de negro a visible).
/// Antes de cambiar de escena llamas FadeOut(...) y en el callback cambias de escena.
/// </summary>
public class ScreenFader : MonoBehaviour
{
    [Tooltip("Panel negro que cubre la pantalla. Debe tener un componente CanvasGroup.")]
    public CanvasGroup fadePanel;

    [Tooltip("Duración de la transición en segundos.")]
    public float duracion = 0.5f;

    void Start()
    {
        // Al abrir la escena empezamos en negro y revelamos.
        if (fadePanel != null)
        {
            fadePanel.alpha = 1f;
            fadePanel.blocksRaycasts = true;
            FadeIn();
        }
    }

    /// <summary>De negro a transparente (revela la pantalla).</summary>
    public void FadeIn(Action alTerminar = null)
    {
        if (fadePanel == null) { alTerminar?.Invoke(); return; }

        fadePanel.blocksRaycasts = true;
        fadePanel.DOFade(0f, duracion).OnComplete(() =>
        {
            fadePanel.blocksRaycasts = false; // deja pasar los clics de nuevo
            alTerminar?.Invoke();
        });
    }

    /// <summary>De transparente a negro (oculta la pantalla). Úsalo antes de cambiar de escena.</summary>
    public void FadeOut(Action alTerminar = null)
    {
        if (fadePanel == null) { alTerminar?.Invoke(); return; }

        fadePanel.blocksRaycasts = true; // bloquea clics durante la transición
        fadePanel.DOFade(1f, duracion).OnComplete(() =>
        {
            alTerminar?.Invoke();
        });
    }
}
