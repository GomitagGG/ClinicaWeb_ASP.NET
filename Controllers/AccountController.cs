using Microsoft.AspNetCore.Mvc;
using ClinicaWeb.Models;
using ClinicaWeb.Data;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace ClinicaWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly ClinicaContext _context;
        public AccountController(ClinicaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

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

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Admin");
            return RedirectToAction("Login");
        }
    }
}
