using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace MedOne.Web.Seguridad;

// Indica a qué clínica pertenece el usuario que está usando la app ahora
public class ClinicaActual
{
    private readonly IHttpContextAccessor _http;
    private readonly AuthenticationStateProvider _auth;
    private Guid? _id;
    private bool _resuelto;

    public ClinicaActual(IHttpContextAccessor http, AuthenticationStateProvider auth)
    {
        _http = http;
        _auth = auth;
    }

    public Guid? Id
    {
        get
        {
            if (!_resuelto)
            {
                var valor = ObtenerUsuario()?.FindFirst("clinica_id")?.Value;
                _id = Guid.TryParse(valor, out var id) ? id : null;
                _resuelto = true;
            }
            return _id;
        }
    }

    private ClaimsPrincipal? ObtenerUsuario()
    {
        try
        {
            // Dentro de las páginas Blazor
            return _auth.GetAuthenticationStateAsync().GetAwaiter().GetResult().User;
        }
        catch (InvalidOperationException)
        {
            // Fuera de Blazor (por ejemplo, en el inicio de sesión)
            return _http.HttpContext?.User;
        }
    }
}