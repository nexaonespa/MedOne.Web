using Auth0.AspNetCore.Authentication;
using MedOne.Web;
using MedOne.Web.Components;
using MedOne.Web.Seguridad;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration["Auth0:Domain"];
    options.ClientId = builder.Configuration["Auth0:ClientId"];
    options.ClientSecret = builder.Configuration["Auth0:ClientSecret"];
    options.Scope = "openid profile email"; // pedimos también el email
});

// Agrega rol y clínica (desde nuestra tabla Usuarios) al usuario de Auth0
builder.Services.AddScoped<IClaimsTransformation, UsuarioClaimsTransformation>();

// Sabe a qué clínica pertenece el usuario conectado (usado por el filtro de datos)
builder.Services.AddScoped<ClinicaActual>();

// En la nube (Railway) la app está detrás de un proxy: respetamos el HTTPS original
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownIPNetworks.Clear();
    options.KnownProxies.Clear();
});

builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseForwardedHeaders();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Si inició sesión pero aún no tiene clínica, lo enviamos a crearla
app.Use(async (context, next) =>
{
    var ruta = context.Request.Path;

    var esRutaLibre =
        ruta.StartsWithSegments("/crear-clinica") ||
        ruta.StartsWithSegments("/Account") ||
        ruta.StartsWithSegments("/callback") ||
        ruta.StartsWithSegments("/Error") ||
        ruta.StartsWithSegments("/_blazor") ||
        ruta.StartsWithSegments("/_framework") ||
        ruta.StartsWithSegments("/_content") ||
        Path.HasExtension(ruta.Value); // archivos como .css, .js, imágenes

    var user = context.User;

    if (user.Identity?.IsAuthenticated == true
        && !user.HasClaim(c => c.Type == "clinica_id")
        && !esRutaLibre)
    {
        context.Response.Redirect("/crear-clinica");
        return;
    }

    await next();
});

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorPages();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();