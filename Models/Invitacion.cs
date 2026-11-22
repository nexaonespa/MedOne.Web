namespace MedOne.Web.Models;

// Invitación para que una persona se una a una clínica con un rol
public class Invitacion : IPerteneceAClinica
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ClinicaId { get; set; }
    public Clinica? Clinica { get; set; }

    public string Email { get; set; } = string.Empty; // siempre en minúsculas
    public Rol Rol { get; set; }

    // Solo si el rol es Profesional: a qué profesional corresponde
    public Guid? ProfesionalId { get; set; }
    public Profesional? Profesional { get; set; }

    public bool Aceptada { get; set; }
    public DateTime? FechaAceptacion { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
}