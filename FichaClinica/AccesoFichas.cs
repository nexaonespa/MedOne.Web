using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using MedOne.Web.Models;

namespace MedOne.Web.FichaClinica;

// Reglas de acceso a la información clínica
public static class AccesoFichas
{
    // Profesional vinculado al usuario (null si no atiende pacientes)
    public static Guid? ProfesionalDe(ClaimsPrincipal usuario) =>
        Guid.TryParse(usuario.FindFirst("profesional_id")?.Value, out var id) ? id : null;

    public static bool EsDirectorClinico(ClaimsPrincipal usuario) =>
        usuario.IsInRole(nameof(Rol.DirectorClinico));

    // ¿Puede ver fichas en general? (para mostrar u ocultar enlaces)
    public static bool PuedeVerFichas(ClaimsPrincipal usuario) =>
        EsDirectorClinico(usuario) || ProfesionalDe(usuario) is not null;

    // ¿Puede leer las fichas de ESTE paciente?
    // Director Clínico: todas. Profesional: solo pacientes con los que ha tenido citas.
    public static async Task<bool> PuedeLeerPaciente(AppDbContext db, ClaimsPrincipal usuario, Guid pacienteId)
    {
        if (EsDirectorClinico(usuario))
            return true;

        var profesionalId = ProfesionalDe(usuario);
        if (profesionalId is null)
            return false;

        return await db.Citas.AnyAsync(c =>
            c.PacienteId == pacienteId &&
            c.ProfesionalId == profesionalId &&
            c.Estado != "Cancelada");
    }
}