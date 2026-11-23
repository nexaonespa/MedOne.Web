using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using MedOne.Web.Models;

namespace MedOne.Web.Seguridad;

// Se ejecuta en cada visita: agrega el rol, la clínica y el profesional del usuario
// (tomados de nuestra tabla Usuarios) a la identidad que entrega Auth0.
public class UsuarioClaimsTransformation : IClaimsTransformation
{
    private readonly AppDbContext _db;
    private readonly IHttpContextAccessor _http;

    public UsuarioClaimsTransformation(AppDbContext db, IHttpContextAccessor http)
    {
        _db = db;
        _http = http;
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if (principal.Identity?.IsAuthenticated != true)
            return principal;

        // Evita agregar los datos dos veces
        if (principal.HasClaim(c => c.Type == "clinica_id"))
            return principal;

        // Los archivos (favicon, imágenes, CSS, JS) no necesitan consultar la base de datos
        var ruta = _http.HttpContext?.Request.Path.Value ?? string.Empty;
        if (Path.HasExtension(ruta))
            return principal;

        var auth0Id = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(auth0Id))
            return principal;

        var usuario = await _db.Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Auth0Id == auth0Id);

        // Si no existe en nuestra base, todavía no ha creado ni se ha unido a una clínica
        if (usuario is null)
            return principal;

        var datosMedOne = new ClaimsIdentity();
        datosMedOne.AddClaim(new Claim(ClaimTypes.Role, usuario.Rol.ToString()));

        // El Director Clínico tiene también todas las facultades del Administrador
        if (usuario.Rol == Rol.DirectorClinico)
            datosMedOne.AddClaim(new Claim(ClaimTypes.Role, nameof(Rol.Administrador)));

        datosMedOne.AddClaim(new Claim("clinica_id", usuario.ClinicaId.ToString()));
        datosMedOne.AddClaim(new Claim("usuario_id", usuario.Id.ToString()));

        // Si el usuario atiende pacientes, guardamos a qué profesional corresponde
        if (usuario.ProfesionalId is Guid profesionalId)
            datosMedOne.AddClaim(new Claim("profesional_id", profesionalId.ToString()));

        var copia = principal.Clone();
        copia.AddIdentity(datosMedOne);
        return copia;
    }
}