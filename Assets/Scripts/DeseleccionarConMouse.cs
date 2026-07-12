using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Evita que se vean dos botones resaltados a la vez (uno por teclado "Selected"
/// y otro por mouse "Highlighted").
/// Cuando el mouse entra a este botón, quita la selección de teclado,
/// dejando solo el resaltado del mouse.
///
/// USO: arrastra este script a CADA botón del menú (además de su componente Button).
/// </summary>
public class DeseleccionarConMouse : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Quita el foco de teclado del elemento actualmente seleccionado.
        if (EventSystem.current != null)
            EventSystem.current.SetSelectedGameObject(null);
    }
}