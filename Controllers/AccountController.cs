using Microsoft.AspNetCore.Mvc;
using ClinicaWeb.Models;
using ClinicaWeb.Data;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace ClinicaWeb.Controllers
{
    /// <summary>
    /// Controlador de autenticación. Gestiona el inicio y cierre de sesión de administradores.
    /// La sesión se almacena en memoria con la clave "Admin" (valor: nombre de usuario).
    /// </summary>
    public class AccountController : Controller
    {
        private readonly ClinicaContext _context;

        /// <summary>
        /// Recibe el contexto de base de datos por inyección de dependencias.
        /// </summary>
        public AccountController(ClinicaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET /Account/Login — Muestra el formulario de inicio de sesión.
        /// </summary>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// POST /Account/Login — Valida las credenciales contra la tabla Admins en la BD.
        /// Si son correctas, guarda el usuario en sesión y redirige al inicio.
        /// </summary>
        /// <param name="usuario">Nombre de usuario ingresado en el formulario.</param>
        /// <param name="clave">Contraseña ingresada en el formulario.</param>
        [HttpPost]
        public IActionResult Login(string usuario, string clave)
        {
            var admin = _context.Set<Admin>().FirstOrDefault(a => a.Usuario == usuario && a.Clave == clave);
            if (admin != null)
            {
                HttpContext.Session.SetString("Admin", admin.Usuario);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Error = "Usuario o clave incorrectos";
            return View();
        }

        /// <summary>
        /// GET /Account/Logout — Elimina la sesión del administrador y redirige al login.
        /// </summary>
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Admin");
            return RedirectToAction("Login");
        }
    }
}
