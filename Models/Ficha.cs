namespace MedOne.Web.Models;

public class Ficha : IPerteneceAClinica
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ClinicaId { get; set; }
    public Clinica? Clinica { get; set; }

    // Una ficha por cita
    public Guid CitaId { get; set; }
    public Cita? Cita { get; set; }

    public Guid PacienteId { get; set; }
    public Paciente? Paciente { get; set; }

    // Autor de la ficha: el único que puede editarla
    public Guid ProfesionalId { get; set; }
    public Profesional? Profesional { get; set; }

    public int EspecialidadId { get; set; }
    public Especialidad? Especialidad { get; set; }

    public string DatosJson { get; set; } = "{}"; // contenido de la ficha
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public DateTime? FechaModificacion { get; set; }
}