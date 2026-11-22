namespace MedOne.Web.Models;

public class Profesional : IPerteneceAClinica
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ClinicaId { get; set; }
    public Clinica? Clinica { get; set; }

    public int EspecialidadId { get; set; }
    public Especialidad? Especialidad { get; set; }

    public string Nombre { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int DuracionCitaMinutos { get; set; } = 30; // duración por defecto, configurable por profesional
}