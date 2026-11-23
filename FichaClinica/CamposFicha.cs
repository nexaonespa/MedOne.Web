namespace MedOne.Web.FichaClinica;

// Un campo propio de una especialidad. "Largo" = cuadro de texto grande.
public record CampoExtra(string Clave, string Etiqueta, bool Largo);

public static class CamposFicha
{
    // La clave es el Id de la especialidad
    private static readonly Dictionary<int, CampoExtra[]> PorEspecialidad = new()
    {
        [1] = // Médico
        [
            new("presion", "Presión arterial", false),
            new("pulso", "Pulso (lpm)", false),
            new("temperatura", "Temperatura (°C)", false),
            new("peso", "Peso (kg)", false),
            new("examenFisico", "Examen físico", true),
            new("medicamentos", "Medicamentos indicados", true)
        ],
        [2] = // Psicólogo
        [
            new("estadoMental", "Estado mental y observaciones conductuales", true),
            new("intervenciones", "Técnicas o intervenciones utilizadas", true),
            new("tareas", "Tareas para el paciente", true)
        ],
        [3] = // Kinesiólogo
        [
            new("dolor", "Dolor (escala 0 a 10)", false),
            new("evaluacionFuncional", "Evaluación funcional", true),
            new("tecnicas", "Técnicas aplicadas", true),
            new("ejerciciosCasa", "Ejercicios para la casa", true)
        ],
        [4] = // Fonoaudiólogo
        [
            new("areaTrabajada", "Área trabajada (lenguaje, habla, voz, deglución o audición)", false),
            new("actividades", "Actividades realizadas", true),
            new("logros", "Logros y dificultades observadas", true)
        ],
        [5] = // Terapeuta Ocupacional
        [
            new("areaDesempeno", "Área de desempeño (AVD, juego, escolar o laboral)", false),
            new("actividades", "Actividades realizadas", true),
            new("recomendaciones", "Recomendaciones para la casa o el colegio", true)
        ],
        [6] = // Neuropsicólogo
        [
            new("instrumentos", "Instrumentos aplicados", true),
            new("funcionesCognitivas", "Funciones cognitivas evaluadas y resultados", true),
            new("recomendaciones", "Recomendaciones", true)
        ]
    };

    public static CampoExtra[] Para(int especialidadId) =>
        PorEspecialidad.TryGetValue(especialidadId, out var campos) ? campos : [];
}