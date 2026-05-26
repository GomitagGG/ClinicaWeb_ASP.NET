using Microsoft.EntityFrameworkCore;
using ClinicaWeb.Models;

namespace ClinicaWeb.Data;

public class ClinicaContext : DbContext
{
    public ClinicaContext(DbContextOptions<ClinicaContext> options) : base(options) {}
    public DbSet<Paciente> Pacientes { get; set; }
}