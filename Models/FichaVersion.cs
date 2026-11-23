namespace MedOne.Web.Models;

// Versión anterior de una ficha, guardada automáticamente cada vez que se edita.
// No se puede modificar ni eliminar desde la aplicación.
public class FichaVersion : IPerteneceAClinica
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ClinicaId { get; set; }

    public Guid FichaId { get; set; }
    public string DatosJson { get; set; } = "{}"; // contenido ANTES del cambio

    // Quién hizo el cambio y cuándo
    public Guid? UsuarioId { get; set; }
    public string UsuarioNombre { get; set; } = string.Empty;
    public DateTime FechaReemplazo { get; set; } = DateTime.UtcNow;
}