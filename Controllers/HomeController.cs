using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ClinicaWeb.Models;

namespace ClinicaWeb.Controllers;

/// <summary>
/// Controlador principal de páginas públicas del sitio.
/// No requiere autenticación; cualquier visitante puede acceder a estas rutas.
/// </summary>
public class HomeController : Controller
{
    /// <summary>
    /// GET / o GET /Home/Index — Página de inicio con hero y tarjetas de servicios.
    /// Vista: <c>Views/Home/Index.cshtml</c>
    /// </summary>
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// GET /Home/Privacy — Página de política de privacidad.
    /// </summary>
    public IActionResult Privacy()
    {
        return View();
    }

    /// <summary>
    /// GET /Home/Clinica — Página "Sobre nosotros" con misión, visión, valores y ubicación.
    /// Vista: <c>Views/Home/Clinica.cshtml</c>
    /// </summary>
    public IActionResult Clinica()
    {
        return View();
    }

    /// <summary>
    /// GET /Home/Especialidades — Lista las especialidades médicas disponibles en la clínica.
    /// Vista: <c>Views/Home/Especialidades.cshtml</c>
    /// </summary>
    public IActionResult Especialidades()
    {
        return View();
    }

    /// <summary>
    /// GET /Home/Contacto — Página de contacto con formulario e información de ubicación.
    /// Vista: <c>Views/Home/Contacto.cshtml</c>
    /// </summary>
    public IActionResult Contacto()
    {
        return View();
    }

    /// <summary>
    /// GET /Home/Error — Página de error genérico.
    /// Sin caché para evitar que el navegador muestre un error desactualizado.
    /// </summary>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
