using System.Text.Json;

namespace MedOne.Web.FichaClinica;

// Contenido de una ficha: campos comunes + campos propios de cada especialidad
public class ContenidoFicha
{
    public string MotivoConsulta { get; set; } = string.Empty;
    public string Evolucion { get; set; } = string.Empty;
    public string Diagnostico { get; set; } = string.Empty;
    public string Plan { get; set; } = string.Empty;

    // Campos de la especialidad (clave → valor)
    public Dictionary<string, string> Extras { get; set; } = new();

    public string ToJson() => JsonSerializer.Serialize(this);

    public static ContenidoFicha DesdeJson(string json)
    {
        try
        {
            return JsonSerializer.Deserialize<ContenidoFicha>(json) ?? new ContenidoFicha();
        }
        catch (JsonException)
        {
            return new ContenidoFicha();
        }
    }
}