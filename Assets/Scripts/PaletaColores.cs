using UnityEngine;

/// <summary>
/// Paleta de colores del sistema visual neobrutalista de EnglishFlow.
/// Cualquier script que necesite aplicar color (feedback de acierto/error,
/// resaltados, etc.) debe usar estas constantes en vez de escribir hex a mano,
/// para que la app se vea coherente en todas las pantallas.
/// </summary>
public static class PaletaColores
{
    public static readonly Color Tinta    = new Color32(26, 26, 26, 255);    // negro casi puro: bordes, sombras, texto
    public static readonly Color Fondo    = new Color32(255, 248, 235, 255); // crema: fondo de pantallas
    public static readonly Color Primario = new Color32(77, 97, 252, 255);   // azul: botones principales
    public static readonly Color Acento   = new Color32(255, 92, 138, 255);  // rosa: elementos secundarios / resaltados
    public static readonly Color Exito    = new Color32(61, 220, 132, 255);  // verde: respuesta correcta
    public static readonly Color Error    = new Color32(255, 59, 59, 255);   // rojo: respuesta incorrecta
    public static readonly Color Resalte  = new Color32(255, 183, 3, 255);   // ámbar: selección activa / foco de teclado
    public static readonly Color Blanco   = Color.white;
}
