using System.ComponentModel.DataAnnotations;

namespace ClinicaWeb.Models;

public class Admin
{
    public int Id { get; set; }
    [Required] public string Usuario { get; set; } = "";
    [Required] public string Clave { get; set; } = "";
}
