namespace MedOne.Web.Models;

// Mensaje enviado desde el formulario de contacto de la portada.
// No pertenece a ninguna clínica: lo revisa el equipo de MedOne.
public class SolicitudContacto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nombre { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string TipoCentro { get; set; } = string.Empty; // Clínica, Centro médico o Profesional independiente
    public string Mensaje { get; set; } = string.Empty;
    public bool AceptaContacto { get; set; }               // consentimiento para usar los datos
    public bool Atendida { get; set; }                     // para marcarla cuando se responda
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
}
