using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Controla el menú principal (Requisito 1).
/// Cada botón de módulo llama a un método público de aquí (se conecta en el Inspector).
/// La navegación por teclado la da el sistema de eventos de Unity + este script,
/// que selecciona el primer botón al abrir la escena.
/// </summary>
public class MenuPrincipal : MonoBehaviour
{
    [Tooltip("El script de transiciones fade de esta escena.")]
    public ScreenFader fader;

    [Tooltip("Primer botón que queda seleccionado para navegar con teclado (flechas/Tab).")]
    public GameObject primerBotonSeleccionado;

    void Start()
    {
        // Para que el teclado funcione, debe haber un botón seleccionado de inicio.
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
