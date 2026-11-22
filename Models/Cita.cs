namespace MedOne.Web.Models;

public class Cita : IPerteneceAClinica
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ClinicaId { get; set; }
    public Clinica? Clinica { get; set; }

    public Guid ProfesionalId { get; set; }
    public Profesional? Profesional { get; set; }

    public Guid PacienteId { get; set; }
    public Paciente? Paciente { get; set; }

    public DateTime FechaHoraInicio { get; set; }
    public int DuracionMinutos { get; set; }
    public string Estado { get; set; } = "Agendada"; // Agendada, Confirmada, Cancelada, Completada
}