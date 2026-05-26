using System.ComponentModel.DataAnnotations;

namespace ClinicaWeb.Models;

public class Paciente
{
    public int Id { get; set; }


    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres")]
    [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ ]+$", ErrorMessage = "El nombre solo puede contener letras y espacios")]
    public string NombreCompleto { get; set; } = "";

    [Required(ErrorMessage = "El RUT es obligatorio")]
    [RegularExpression(@"^(\d{1,2}\.?\d{3}\.?\d{3}-[\dkK])$", ErrorMessage = "El RUT debe tener el formato 12345678-9 o 12.345.678-9")]
    public string Run { get; set; } = "";

    [Range(0, 120, ErrorMessage = "La edad debe estar entre 0 y 120 años")]
    public int Edad { get; set; }

    [Required(ErrorMessage = "El teléfono es obligatorio")]
    [Phone(ErrorMessage = "El teléfono no es válido")]
    [StringLength(20, MinimumLength = 7, ErrorMessage = "El teléfono debe tener entre 7 y 20 dígitos")]
    public string Telefono { get; set; } = "";

    [Required(ErrorMessage = "La dirección es obligatoria")]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "La dirección debe tener entre 5 y 200 caracteres")]
    public string Direccion { get; set; } = "";

    [EmailAddress(ErrorMessage = "El email no es válido")]
    public string Email { get; set; } = "";

    [StringLength(500, ErrorMessage = "El diagnóstico no puede superar los 500 caracteres")]
    public string Diagnostico { get; set; } = "";
}