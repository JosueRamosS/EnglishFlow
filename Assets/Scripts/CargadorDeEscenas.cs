using UnityEngine.SceneManagement;

/// <summary>
/// Punto único para cambiar de escena.
/// Guarda cuál escena queremos cargar y manda a la pantalla de "Carga".
/// La pantalla de Carga (script PantallaDeCarga) lee este destino y hace la carga real.
///
/// Uso desde cualquier botón/script:
///   CargadorDeEscenas.CargarEscena("ModuloVocabulario");
/// </summary>
public static class CargadorDeEscenas
{
    // Aquí se guarda a dónde queremos ir. La pantalla de carga lo lee.
    public static string EscenaDestino { get; private set; }

    /// <summary>
    /// Nombre EXACTO de tu escena de carga tal como aparece en Build Settings.
    /// Si la llamas distinto, cambia este texto.
    /// </summary>
    public const string EscenaDeCarga = "Carga";

    public static void CargarEscena(string nombreEscena)
    {
        EscenaDestino = nombreEscena;
        // Vamos a la pantalla de carga; ella hará la carga asíncrona con barra de progreso.
        SceneManager.LoadScene(EscenaDeCarga);
    }
}
