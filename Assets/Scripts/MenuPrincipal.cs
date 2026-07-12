using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Controla el menú principal (Requisito 1).
/// Cada botón de módulo llama a un método público de aquí (se conecta en el Inspector).
///
/// Navegación teclado + mouse:
/// - Al iniciar, selecciona el primer botón (para poder usar teclado de una).
/// - Si el mouse deseleccionó todo y el usuario pulsa una flecha, se vuelve a
///   seleccionar un botón automáticamente para retomar el control con teclado.
/// </summary>
public class MenuPrincipal : MonoBehaviour
{
    [Tooltip("El script de transiciones fade de esta escena.")]
    public ScreenFader fader;

    [Tooltip("Primer botón que queda seleccionado para navegar con teclado (flechas/Tab).")]
    public GameObject primerBotonSeleccionado;

    void Start()
    {
        SeleccionarPrimerBoton();
    }

    void Update()
    {
        // Si NO hay nada seleccionado (porque el mouse deseleccionó) y el usuario
        // empieza a usar el teclado, reactivamos la selección en el primer botón.
        if (EventSystem.current != null &&
            EventSystem.current.currentSelectedGameObject == null)
        {
            float vertical = Input.GetAxisRaw("Vertical");
            bool tab = Input.GetKeyDown(KeyCode.Tab);

            if (Mathf.Abs(vertical) > 0.1f || tab)
                SeleccionarPrimerBoton();
        }
    }

    void SeleccionarPrimerBoton()
    {
        if (primerBotonSeleccionado != null && EventSystem.current != null)
            EventSystem.current.SetSelectedGameObject(primerBotonSeleccionado);
    }

    // ---- Estos métodos se conectan al evento OnClick de cada botón ----

    public void IrAVocabulario() => Transicionar("ModuloVocabulario");
    public void IrAGramatica()   => Transicionar("ModuloGramatica");
    public void IrALectura()     => Transicionar("ModuloLectura");
    public void IrAEscritura()   => Transicionar("ModuloEscritura");
    public void IrAConfiguracion() => Transicionar("Configuracion");
    public void IrAResumen()     => Transicionar("Resumen");

    /// <summary>Hace fade out y luego carga la escena (que pasa por la pantalla de carga).</summary>
    private void Transicionar(string escena)
    {
        if (fader != null)
            fader.FadeOut(() => CargadorDeEscenas.CargarEscena(escena));
        else
            CargadorDeEscenas.CargarEscena(escena);
    }

    public void SalirDelJuego()
    {
        Application.Quit();
    }
}