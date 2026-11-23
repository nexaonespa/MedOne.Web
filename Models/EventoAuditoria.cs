namespace MedOne.Web.Models;

// Registro de auditoría: no se puede modificar ni eliminar desde la aplicación.
// No tiene enlaces a otras tablas a propósito, para que el registro se conserve
// aunque se borren los datos a los que se refiere.
public class EventoAuditoria : IPerteneceAClinica
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ClinicaId { get; set; }

    // Quién hizo la acción
    public Guid? UsuarioId { get; set; }
    public string UsuarioNombre { get; set; } = string.Empty;

    // Qué pasó (por ejemplo: "CambioUsuario", "InvitacionCreada", "AccesoFicha")
    public string Tipo { get; set; } = string.Empty;
    public string Detalle { get; set; } = string.Empty;

    // Sobre qué paciente o ficha (si aplica)
    public Guid? PacienteId { get; set; }
    public Guid? FichaId { get; set; }

    public DateTime Fecha { get; set; } = DateTime.UtcNow;
}