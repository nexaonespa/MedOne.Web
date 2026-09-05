namespace MedOne.Web.Models;

public class Clinica
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nombre { get; set; } = string.Empty;
    public string Rut { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    public List<Profesional> Profesionales { get; set; } = new();
    public List<Paciente> Pacientes { get; set; } = new();
}