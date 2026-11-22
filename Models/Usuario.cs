namespace MedOne.Web.Models;

public class Usuario
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Auth0Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public Rol Rol { get; set; }

    public Guid ClinicaId { get; set; }
    public Clinica? Clinica { get; set; }

    public Guid? ProfesionalId { get; set; }
    public Profesional? Profesional { get; set; }

    public Guid? PacienteId { get; set; }
    public Paciente? Paciente { get; set; }

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
}