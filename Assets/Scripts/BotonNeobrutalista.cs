using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections;

/// <summary>
/// Efecto de botón estilo neobrutalista (Fase 2, sistema visual).
/// El botón tiene una "Cara" (rectángulo con borde negro) que descansa encima
/// de una "Sombra" (rectángulo negro fijo, desplazado). Al presionar, la Cara
/// se desliza hasta cubrir la Sombra por completo, dando la sensación de que
/// el botón se hunde. Al soltar, vuelve a su posición flotante.
///
/// También muestra el foco de teclado/mando: mientras el botón está
/// seleccionado (EventSystem.currentSelectedGameObject), el borde cambia a
/// amarillo (PaletaColores.Resalte) en vez de negro.
///
/// USO: va en el mismo GameObject que el componente Button, junto a los
/// hijos "Sombra" y "Cara" (ver guía de editor). El campo "cara" se autocompleta
/// si el hijo se llama exactamente "Cara".
/// </summary>
public class BotonNeobrutalista : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, ISubmitHandler, ISelectHandler, IDeselectHandler
{
    [Tooltip("El rectángulo 'Cara' del botón (el que tiene el borde negro y se mueve).")]
    public RectTransform cara;

    [Tooltip("Cuánto se desplaza la Cara para tapar la Sombra al presionar (debe coincidir con la posición de la Sombra).")]
    public Vector2 desplazamientoPresionado = new Vector2(8f, -8f);

    [Tooltip("Duración de la animación de presionar/soltar y del cambio de color de foco.")]
    public float duracion = 0.08f;

    bool presionado = false;
    Image imagenBorde;

    void Awake()
    {
        // La "Cara" es el rectángulo negro que hace de borde: tomamos su Image
        // para poder animarle el color cuando el botón gana/pierde el foco.
        if (cara != null) imagenBorde = cara.GetComponent<Image>();
    }

    void Reset()
    {
        // Autocompletar la referencia si el hijo se llama "Cara".
        Transform hijo = transform.Find("Cara");
        if (hijo != null) cara = hijo.GetComponent<RectTransform>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (imagenBorde != null) imagenBorde.DOColor(PaletaColores.Resalte, duracion);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (imagenBorde != null) imagenBorde.DOColor(PaletaColores.Tinta, duracion);
    }

    public void OnPointerDown(PointerEventData eventData) => Presionar();
    public void OnPointerUp(PointerEventData eventData) => Soltar();

    public void OnPointerExit(PointerEventData eventData)
    {
        // Si el mouse sale mientras estaba presionado, soltamos para no dejarlo "hundido".
        if (presionado) Soltar();
    }

    // OnSubmit (Enter/Espacio con teclado o mando) llega de un solo golpe, sin
    // down/up separados, así que simulamos la secuencia completa presionar->soltar.
    public void OnSubmit(BaseEventData eventData) => StartCoroutine(PresionarYSoltar());

    IEnumerator PresionarYSoltar()
    {
        Presionar();
        yield return new WaitForSeconds(duracion + 0.05f);
        Soltar();
    }

    void Presionar()
    {
        if (cara == null || presionado) return;
        presionado = true;
        cara.DOAnchorPos(desplazamientoPresionado, duracion).SetEase(Ease.OutQuad);
    }

    void Soltar()
    {
        if (cara == null || !presionado) return;
        presionado = false;
        cara.DOAnchorPos(Vector2.zero, duracion).SetEase(Ease.OutQuad);
    }
}
