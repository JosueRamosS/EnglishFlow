using System.Collections.Generic;

/// <summary>
/// Un par palabra-definición para el módulo de Vocabulario.
/// </summary>
public class ParVocabulario
{
    public string palabra;
    public string definicion;

    public ParVocabulario(string palabra, string definicion)
    {
        this.palabra = palabra;
        this.definicion = definicion;
    }
}

/// <summary>
/// Banco de palabras del módulo de Vocabulario (Requisito 4), separado por nivel B1/B2.
///
/// FUENTE del vocabulario: "The Oxford 3000 by CEFR level" (Oxford University Press),
/// lista pública que etiqueta las ~3000 palabras más importantes del inglés según su
/// nivel del Marco Común Europeo (A1-B2). Es la misma referencia que usan editoriales
/// como Cambridge/Oxford para graduar exámenes tipo PET (B1) y FCE (B2).
/// PDF oficial: https://www.oxfordlearnersdictionaries.com/external/pdf/wordlists/oxford-3000-5000/The_Oxford_3000_by_CEFR_level.pdf
///
/// El PDF solo lista palabra + categoría gramatical (no trae definiciones), así que
/// las definiciones en inglés simple de abajo son redacción propia, estilo diccionario
/// para estudiantes (definir con vocabulario más básico que la palabra definida).
/// </summary>
public static class BancoVocabulario
{
    public static readonly List<ParVocabulario> B1 = new List<ParVocabulario>
    {
        new ParVocabulario("earthquake", "a sudden, violent shaking of the ground"),
        new ParVocabulario("economy", "the system by which a country produces and uses money and goods"),
        new ParVocabulario("flood", "a large amount of water covering an area that is usually dry"),
        new ParVocabulario("friendship", "a relationship between people who like and trust each other"),
        new ParVocabulario("countryside", "land with fields, farms and villages, away from towns and cities"),
        new ParVocabulario("currency", "the type of money that a country uses"),
        new ParVocabulario("emergency", "a sudden, dangerous situation that needs action right away"),
        new ParVocabulario("ingredient", "one of the foods used to make a particular dish"),
        new ParVocabulario("entertainment", "films, music, shows, and other things that amuse people"),
        new ParVocabulario("generation", "all the people who were born around the same time"),
        new ParVocabulario("fuel", "a material, like petrol or gas, that is burned to produce power"),
        new ParVocabulario("garage", "a building where a car is kept or repaired"),
        new ParVocabulario("ghost", "the spirit of a dead person that some believe can appear"),
        new ParVocabulario("journal", "a magazine about a specific subject, or a personal diary"),
        new ParVocabulario("election", "a process where people vote to choose a leader"),
        new ParVocabulario("cottage", "a small house, usually in the countryside"),
        new ParVocabulario("costume", "special clothes worn to look like someone or something else"),
        new ParVocabulario("employment", "having a paid job"),
    };

    public static readonly List<ParVocabulario> B2 = new List<ParVocabulario>
    {
        new ParVocabulario("aircraft", "a machine that can fly, such as an airplane"),
        new ParVocabulario("budget", "a plan for how much money you will spend and save"),
        new ParVocabulario("debt", "money that a person or country owes to someone else"),
        new ParVocabulario("defence", "actions taken to protect someone or something from attack"),
        new ParVocabulario("decoration", "something added to make a place look more attractive"),
        new ParVocabulario("examination", "a formal test of knowledge, or a medical check-up"),
        new ParVocabulario("executive", "a person with a senior position in a company"),
        new ParVocabulario("agenda", "a list of things to be discussed at a meeting"),
        new ParVocabulario("cancer", "a serious disease caused by cells growing abnormally"),
        new ParVocabulario("capacity", "the maximum amount that something can hold or produce"),
        new ParVocabulario("characteristic", "a typical feature that helps identify someone or something"),
        new ParVocabulario("bullet", "a small piece of metal fired from a gun"),
        new ParVocabulario("aggressive", "ready to attack or behave in a hostile way"),
        new ParVocabulario("accurate", "completely correct and exact"),
        new ParVocabulario("bitter", "having a sharp, unpleasant taste; or feeling angry and resentful"),
        new ParVocabulario("broad", "wide, or covering a large range of things"),
        new ParVocabulario("brief", "lasting only a short time"),
        new ParVocabulario("capable", "having the skill or ability to do something"),
    };

    /// <summary>Devuelve la lista correspondiente al nivel guardado en GameProgress ("B1" o "B2").</summary>
    public static List<ParVocabulario> ListaSegunNivel()
    {
        return GameProgress.ObtenerNivel() == "B2" ? B2 : B1;
    }
}
