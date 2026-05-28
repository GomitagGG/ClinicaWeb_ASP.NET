using Microsoft.EntityFrameworkCore;
using ClinicaWeb.Models;

namespace ClinicaWeb.Data;

/// <summary>
/// Contexto de base de datos principal de la clínica.
/// Se conecta a MySQL usando la cadena de conexión "DefaultConnection" definida en appsettings.json.
/// Registrado en Program.cs via <c>builder.Services.AddDbContext&lt;ClinicaContext&gt;</c>.
/// </summary>
public class ClinicaContext : DbContext
{
    /// <summary>
    /// Inicializa el contexto con las opciones inyectadas por el contenedor de dependencias.
    /// </summary>
    /// <param name="options">Opciones de configuración de EF Core (proveedor MySQL, cadena de conexión).</param>
    public ClinicaContext(DbContextOptions<ClinicaContext> options) : base(options) {}

    /// <summary>Tabla de pacientes registrados en la clínica.</summary>
    public DbSet<Paciente> Pacientes { get; set; }

    /// <summary>Tabla de administradores con acceso al sistema.</summary>
    public DbSet<Admin> Admins { get; set; }
}