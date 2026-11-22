namespace MedOne.Web.Models;

// Marca los modelos cuyos datos pertenecen a una clínica específica
public interface IPerteneceAClinica
{
    Guid ClinicaId { get; set; }
}
