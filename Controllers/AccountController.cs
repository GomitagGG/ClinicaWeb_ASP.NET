using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using ClinicaWeb.Data;

namespace ClinicaWeb.Controllers;

public class AccountController : Controller
{
    private readonly DbConnectionFactory _factory;

    public AccountController(DbConnectionFactory factory)
    {
        _factory = factory;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string usuario, string clave)
    {
        using var conn = _factory.Create();
        await conn.OpenAsync();
        using var cmd = new MySqlCommand(
            "SELECT Usuario FROM Admins WHERE Usuario=@Usuario AND Clave=@Clave LIMIT 1", conn);
        cmd.Parameters.AddWithValue("@Usuario", usuario);
        cmd.Parameters.AddWithValue("@Clave",   clave);
        var resultado = await cmd.ExecuteScalarAsync();
        if (resultado != null)
        {
            HttpContext.Session.SetString("Admin", resultado.ToString()!);
            return RedirectToAction("Index", "Home");
        }
        ViewBag.Error = "Usuario o clave incorrectos";
        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Remove("Admin");
        return RedirectToAction("Login");
    }
}
