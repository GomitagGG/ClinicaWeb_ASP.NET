using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using ClinicaWeb.Data;
using ClinicaWeb.Models;

namespace ClinicaWeb.Controllers;

public class PacientesController : Controller
{
    private readonly DbConnectionFactory _factory;

    public PacientesController(DbConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<IActionResult> Index()
    {
        return View(await ObtenerPacientesAsync());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Paciente paciente)
    {
        if (ModelState.IsValid)
        {
            using var conn = _factory.Create();
            await conn.OpenAsync();
            using var cmd = new MySqlCommand(
                "INSERT INTO Pacientes (NombreCompleto, Run, Edad, Telefono, Direccion, Email, Diagnostico) " +
                "VALUES (@NombreCompleto, @Run, @Edad, @Telefono, @Direccion, @Email, @Diagnostico)", conn);
            AgregarParametros(cmd, paciente);
            await cmd.ExecuteNonQueryAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["AbrirModal"] = "Crear";
        return View("Index", await ObtenerPacientesAsync());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Paciente paciente)
    {
        if (ModelState.IsValid)
        {
            using var conn = _factory.Create();
            await conn.OpenAsync();
            using var cmd = new MySqlCommand(
                "UPDATE Pacientes SET NombreCompleto=@NombreCompleto, Run=@Run, Edad=@Edad, " +
                "Telefono=@Telefono, Direccion=@Direccion, Email=@Email, Diagnostico=@Diagnostico " +
                "WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@Id", paciente.Id);
            AgregarParametros(cmd, paciente);
            await cmd.ExecuteNonQueryAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["AbrirModal"] = "Editar";
        return View("Index", await ObtenerPacientesAsync());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        using var conn = _factory.Create();
        await conn.OpenAsync();
        using var cmd = new MySqlCommand("DELETE FROM Pacientes WHERE Id=@Id", conn);
        cmd.Parameters.AddWithValue("@Id", id);
        await cmd.ExecuteNonQueryAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task<List<Paciente>> ObtenerPacientesAsync()
    {
        var lista = new List<Paciente>();
        using var conn = _factory.Create();
        await conn.OpenAsync();
        using var cmd = new MySqlCommand(
            "SELECT Id, NombreCompleto, Run, Edad, Telefono, Direccion, Email, Diagnostico FROM Pacientes", conn);
        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            lista.Add(new Paciente
            {
                Id          = reader.GetInt32("Id"),
                NombreCompleto = reader.GetString("NombreCompleto"),
                Run         = reader.GetString("Run"),
                Edad        = reader.GetInt32("Edad"),
                Telefono    = reader.GetString("Telefono"),
                Direccion   = reader.GetString("Direccion"),
                Email       = reader.IsDBNull(reader.GetOrdinal("Email"))       ? "" : reader.GetString("Email"),
                Diagnostico = reader.IsDBNull(reader.GetOrdinal("Diagnostico")) ? "" : reader.GetString("Diagnostico")
            });
        }
        return lista;
    }

    private static void AgregarParametros(MySqlCommand cmd, Paciente p)
    {
        cmd.Parameters.AddWithValue("@NombreCompleto", p.NombreCompleto);
        cmd.Parameters.AddWithValue("@Run",            p.Run);
        cmd.Parameters.AddWithValue("@Edad",           p.Edad);
        cmd.Parameters.AddWithValue("@Telefono",       p.Telefono);
        cmd.Parameters.AddWithValue("@Direccion",      p.Direccion);
        cmd.Parameters.AddWithValue("@Email",          p.Email);
        cmd.Parameters.AddWithValue("@Diagnostico",    p.Diagnostico);
    }
}
