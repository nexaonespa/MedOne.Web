namespace MedOne.Web.Utilidades;

// Convierte entre la hora de Chile (la que ve el usuario) y UTC (la que guarda la base de datos).
// Considera automáticamente el horario de verano/invierno.
public static class HoraChile
{
    private static readonly TimeZoneInfo Zona =
        TimeZoneInfo.FindSystemTimeZoneById("America/Santiago");

    // De UTC (base de datos) a hora de Chile (pantalla)
    public static DateTime ALocal(DateTime utc) =>
        TimeZoneInfo.ConvertTimeFromUtc(DateTime.SpecifyKind(utc, DateTimeKind.Utc), Zona);

    // De hora de Chile (pantalla) a UTC (base de datos)
    public static DateTime AUtc(DateTime local) =>
        TimeZoneInfo.ConvertTimeToUtc(DateTime.SpecifyKind(local, DateTimeKind.Unspecified), Zona);

    // Fecha y hora actual en Chile
    public static DateTime Ahora => ALocal(DateTime.UtcNow);
}