using System.ComponentModel.DataAnnotations;

namespace ClinicaWeb.Models;

/// <summary>
/// Modelo que representa a un administrador del sistema.
/// Mapeado a la tabla <c>Admins</c> en la base de datos MySQL a través de <see cref="ClinicaWeb.Data.ClinicaContext"/>.
/// Se usa para validar el inicio de sesión en <c>AccountController.Login</c>.
/// </summary>
public class Admin
{
    /// <summary>Identificador único del administrador (clave primaria, autogenerada por la BD).</summary>
    public int Id { get; set; }

    /// <summary>Nombre de usuario para iniciar sesión. Campo obligatorio.</summary>
    [Required] public string Usuario { get; set; } = "";

    /// <summary>Contraseña del administrador. Campo obligatorio.</summary>
    [Required] public string Clave { get; set; } = "";
}
