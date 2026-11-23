namespace MedOne.Web.Models;

public enum Rol
{
    Administrador,
    Coordinador,
    Profesional,
    Paciente,
    DirectorClinico // solo lo asigna MedOne; tiene facultades de Administrador + acceso a todas las fichas
}