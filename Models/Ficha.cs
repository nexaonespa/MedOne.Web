namespace MedOne.Web.Models;

public class Ficha
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CitaId { get; set; }
    public Cita? Cita { get; set; }

    public Guid PacienteId { get; set; }
    public Paciente? Paciente { get; set; }

    public int EspecialidadId { get; set; }
    public Especialidad? Especialidad { get; set; }

    public string DatosJson { get; set; } = "{}"; // contenido específico de cada especialidad
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
}