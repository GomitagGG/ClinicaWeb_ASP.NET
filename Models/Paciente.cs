using System.ComponentModel.DataAnnotations;

namespace ClinicaWeb.Models;

public class Paciente
{
    public int Id { get; set; }
    [Required] public string NombreCompleto { get; set; } = "";
    [Required] public string Run { get; set; } = "";
    [Range(0, 120)] public int Edad { get; set; }
    public string Telefono { get; set; } = "";
    public string Direccion { get; set; } = "";
    [EmailAddress] public string Email { get; set; } = "";
    public string Diagnostico { get; set; } = "";
}