using System.Collections.Generic;

/// <summary>
/// Una pregunta de opción múltiple para el módulo de Gramática.
/// "oracion" debe contener "___" en el lugar del espacio en blanco.
/// </summary>
public class PreguntaGramatica
{
    public string oracion;
    public string[] opciones;
    public int indiceCorrecta;

    public PreguntaGramatica(string oracion, string[] opciones, int indiceCorrecta)
    {
        this.oracion = oracion;
        this.opciones = opciones;
        this.indiceCorrecta = indiceCorrecta;
    }
}

/// <summary>
/// Banco de preguntas del módulo de Gramática (Requisito 5), separado por nivel B1/B2.
///
/// FUENTE de los puntos gramaticales: "British Council – EAQUALS Core Inventory for
/// General English" (2nd edition, 2015), un documento público desarrollado junto con
/// EAQUALS que mapea qué estructuras gramaticales corresponde enseñar en cada nivel
/// del Marco Común Europeo (A1-C1). Es un estándar de referencia usado por academias
/// de inglés para diseñar programas de estudio.
/// PDF: https://www.teachingenglish.org.uk/sites/teacheng/files/pub-british-council-eaquals-core-inventoryv2.pdf
///
/// De esa tabla tomé los puntos listados para B1 (Threshold Level) y B2 (Vantage Level)
/// -por ejemplo B1: pasado simple/continuo, voz pasiva simple, modales de deducción,
/// reported speech, condicionales 2do/3ro; B2: pasado perfecto (simple/continuo),
/// voz pasiva en más tiempos, modales en pasado, cláusulas relativas, wish- y con eso
/// redacté yo mismo las oraciones de ejemplo y sus 3 opciones (el documento fuente
/// no trae oraciones de práctica, solo la lista de estructuras).
/// </summary>
public static class BancoGramatica
{
    public static readonly List<PreguntaGramatica> B1 = new List<PreguntaGramatica>
    {
        new PreguntaGramatica("She ___ to the cinema last night.",
            new [] { "goes", "went", "has gone" }, 1),

        new PreguntaGramatica("While I ___ dinner, the phone rang.",
            new [] { "cooked", "was cooking", "had cooked" }, 1),

        new PreguntaGramatica("This bridge ___ in 1990.",
            new [] { "built", "was built", "is building" }, 1),

        new PreguntaGramatica("You ___ be tired — you just woke up from a nine-hour sleep.",
            new [] { "must", "can't", "should" }, 1),

        new PreguntaGramatica("Take an umbrella, it ___ rain later.",
            new [] { "must", "might", "can't" }, 1),

        new PreguntaGramatica("She said she ___ tired.",
            new [] { "is", "was", "has been" }, 1),

        new PreguntaGramatica("If I ___ more money, I would travel the world.",
            new [] { "have", "had", "will have" }, 1),

        new PreguntaGramatica("If she had studied harder, she ___ the exam.",
            new [] { "would pass", "would have passed", "passed" }, 1),

        new PreguntaGramatica("This coffee is ___ hot to drink right now.",
            new [] { "enough", "too", "so" }, 1),

        new PreguntaGramatica("___ it was raining, we went for a walk.",
            new [] { "Because", "Although", "So" }, 1),

        new PreguntaGramatica("This time tomorrow, I ___ on a beach in Cancun.",
            new [] { "will lie", "will be lying", "lie" }, 1),

        new PreguntaGramatica("You haven't seen my keys, ___ you?",
            new [] { "have", "haven't", "did" }, 0),
    };

    public static readonly List<PreguntaGramatica> B2 = new List<PreguntaGramatica>
    {
        new PreguntaGramatica("By next year, I ___ my degree.",
            new [] { "will finish", "will have finished", "finish" }, 1),

        new PreguntaGramatica("If I hadn't missed the flight, I ___ here with you now.",
            new [] { "would be", "would have been", "will be" }, 0),

        new PreguntaGramatica("He ___ have taken the wrong bus — he's not answering his phone.",
            new [] { "must", "should", "can" }, 0),

        new PreguntaGramatica("You ___ have worried, everything was fine.",
            new [] { "mustn't", "needn't", "can't" }, 1),

        new PreguntaGramatica("She isn't here yet; she ___ have forgotten about the meeting, or maybe she's stuck in traffic.",
            new [] { "might", "must", "needn't" }, 0),

        new PreguntaGramatica("When we arrived at the station, the train ___ already.",
            new [] { "left", "had left", "was leaving" }, 1),

        new PreguntaGramatica("The report ___ by the time you arrive.",
            new [] { "will finish", "will have finished", "will have been finished" }, 2),

        new PreguntaGramatica("I ___ never seen snow before I moved to Canada.",
            new [] { "have", "had", "was" }, 1),

        new PreguntaGramatica("They ___ for three hours when the rescue team finally arrived.",
            new [] { "were walking", "had been walking", "have walked" }, 1),

        new PreguntaGramatica("My brother, ___ lives in Madrid, is visiting us next week.",
            new [] { "that", "who", "which" }, 1),

        new PreguntaGramatica("I wish I ___ more time to finish this project.",
            new [] { "have", "had", "will have" }, 1),

        new PreguntaGramatica("When we were kids, we ___ spend every summer at our grandparents' house.",
            new [] { "would", "will", "are" }, 0),
    };

    /// <summary>Devuelve la lista correspondiente al nivel guardado en GameProgress ("B1" o "B2").</summary>
    public static List<PreguntaGramatica> ListaSegunNivel()
    {
        return GameProgress.ObtenerNivel() == "B2" ? B2 : B1;
    }
}
