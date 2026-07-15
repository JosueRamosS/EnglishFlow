using UnityEngine;
using System;

/// <summary>
/// Servicio central de progreso y configuración.
/// Todo se guarda en PlayerPrefs (almacenamiento local de Unity).
/// Es una clase estática: se llama desde cualquier script sin instanciar.
/// Ej: GameProgress.GuardarPuntaje("vocabulario", 80);
/// </summary>
public static class GameProgress
{
    // ---------- CONFIGURACIÓN (Requisito 2) ----------

    // Nivel de inglés: "B1" o "B2"
    public static void GuardarNivel(string nivel) => PlayerPrefs.SetString("cfg_nivel", nivel);
    public static string ObtenerNivel() => PlayerPrefs.GetString("cfg_nivel", "B1");

    // Idioma de instrucciones: "es" o "en"
    public static void GuardarIdioma(string idioma) => PlayerPrefs.SetString("cfg_idioma", idioma);
    public static string ObtenerIdioma() => PlayerPrefs.GetString("cfg_idioma", "es");

    // Volumen 0.0 a 1.0
    public static void GuardarVolumen(float v) => PlayerPrefs.SetFloat("cfg_volumen", v);
    public static float ObtenerVolumen() => PlayerPrefs.GetFloat("cfg_volumen", 1f);

    // ---------- PROGRESO POR MÓDULO (Requisito 8) ----------
    // Usamos el nombre del módulo como parte de la clave.
    // Módulos esperados: "vocabulario", "gramatica", "lectura", "escritura"

    /// <summary>Guarda el puntaje solo si es mejor que el anterior.</summary>
    public static void GuardarPuntaje(string modulo, int puntaje)
    {
        string clave = "punt_" + modulo;
        int actual = PlayerPrefs.GetInt(clave, 0);
        if (puntaje > actual)
            PlayerPrefs.SetInt(clave, puntaje);
        PlayerPrefs.Save();
    }

    public static int ObtenerPuntaje(string modulo) => PlayerPrefs.GetInt("punt_" + modulo, 0);

    /// <summary>Suma 1 al contador de intentos de ese módulo.</summary>
    public static void RegistrarIntento(string modulo)
    {
        string clave = "int_" + modulo;
        int actual = PlayerPrefs.GetInt(clave, 0);
        PlayerPrefs.SetInt(clave, actual + 1);
        PlayerPrefs.Save();
    }

    public static int ObtenerIntentos(string modulo) => PlayerPrefs.GetInt("int_" + modulo, 0);

    /// <summary>Porcentaje de dominio (0-100). Guarda el mejor.</summary>
    public static void GuardarDominio(string modulo, int porcentaje)
    {
        porcentaje = Mathf.Clamp(porcentaje, 0, 100);
        string clave = "dom_" + modulo;
        int actual = PlayerPrefs.GetInt(clave, 0);
        if (porcentaje > actual)
            PlayerPrefs.SetInt(clave, porcentaje);
        PlayerPrefs.Save();
    }

    public static int ObtenerDominio(string modulo) => PlayerPrefs.GetInt("dom_" + modulo, 0);

    // ---------- RACHA DE DÍAS (Requisito 10) ----------

    /// <summary>
    /// Llamar una vez cuando el usuario practica.
    /// Si practicó ayer -> racha +1. Si es el mismo día -> no cambia.
    /// Si pasó más de un día -> racha se reinicia a 1.
    /// </summary>
    public static void ActualizarRacha()
    {
        string hoy = DateTime.Now.ToString("yyyy-MM-dd");
        string ultimaFecha = PlayerPrefs.GetString("racha_fecha", "");
        int racha = PlayerPrefs.GetInt("racha_dias", 0);

        if (ultimaFecha == hoy)
            return; // ya se contó hoy, no hacemos nada

        if (!string.IsNullOrEmpty(ultimaFecha) &&
            DateTime.TryParse(ultimaFecha, out DateTime fechaAnterior))
        {
            int diferencia = (DateTime.Now.Date - fechaAnterior.Date).Days;
            racha = (diferencia == 1) ? racha + 1 : 1;
        }
        else
        {
            racha = 1; // primera vez que practica
        }

        PlayerPrefs.SetString("racha_fecha", hoy);
        PlayerPrefs.SetInt("racha_dias", racha);
        PlayerPrefs.Save();
    }

    public static int ObtenerRacha() => PlayerPrefs.GetInt("racha_dias", 0);

    // ---------- UTILIDAD ----------

    /// <summary>Borra TODO el progreso. Útil para probar desde cero.</summary>
    public static void ReiniciarTodo()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Devuelve el módulo con menor dominio ENTRE los que ya se intentaron al menos
    /// una vez (si un módulo nunca se jugó, su dominio es 0 por defecto y no sería
    /// justo "recomendarlo" solo por eso). Si todavía no se practicó nada, recomienda
    /// "vocabulario" como punto de partida.
    /// </summary>
    public static string ModuloARecomendar()
    {
        string[] modulos = { "vocabulario", "gramatica", "lectura", "escritura" };
        string peor = "vocabulario";
        int menor = 101;
        foreach (string m in modulos)
        {
            if (ObtenerIntentos(m) == 0) continue;
            int dom = ObtenerDominio(m);
            if (dom < menor)
            {
                menor = dom;
                peor = m;
            }
        }
        return peor;
    }
}
