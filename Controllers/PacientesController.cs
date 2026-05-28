using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClinicaWeb.Data;
using ClinicaWeb.Models;

namespace ClinicaWeb.Controllers;

/// <summary>
/// Controlador CRUD de pacientes. Requiere sesión de administrador activa
/// (el middleware en Program.cs redirige a /Account/Login si no hay sesión).
/// Todas las operaciones de escritura usan <see cref="ValidateAntiForgeryToken"/> contra CSRF.
/// </summary>
public class PacientesController : Controller
{
    private readonly ClinicaContext _context;

    /// <summary>
    /// Recibe el contexto de base de datos por inyección de dependencias.
    /// </summary>
    public PacientesController(ClinicaContext context)
    {
        _context = context;
    }

    /// <summary>
    /// GET /Pacientes — Recupera todos los pacientes de la BD y los muestra en la vista Index.
    /// </summary>
    public async Task<IActionResult> Index()
    {
        return View(await _context.Pacientes.ToListAsync());
    }

    /// <summary>
    /// POST /Pacientes/Create — Inserta un nuevo paciente en la BD si el modelo es válido.
    /// </summary>
    /// <param name="paciente">Datos del paciente enviados desde el formulario.</param>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Paciente paciente)
    {
        if (ModelState.IsValid)
        {
            _context.Add(paciente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // Si hay errores de validación, regresar la vista con los errores y abrir el modal de crear
        ViewData["AbrirModal"] = "Crear";
        var pacientes = await _context.Pacientes.ToListAsync();
        return View("Index", pacientes);
    }

    /// <summary>
    /// POST /Pacientes/Edit/{id} — Actualiza un paciente existente en la BD.
    /// Retorna 404 si el id de la URL no coincide con el del modelo.
    /// </summary>
    /// <param name="id">Identificador del paciente a editar (viene de la URL).</param>
    /// <param name="paciente">Datos actualizados enviados desde el formulario.</param>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Paciente paciente)
    {
        if (ModelState.IsValid)
        {
            _context.Update(paciente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // Si hay errores de validación, regresar la vista con los errores y abrir el modal de editar
        ViewData["AbrirModal"] = "Editar";
        var pacientes = await _context.Pacientes.ToListAsync();
        return View("Index", pacientes);
    }

    /// <summary>
    /// POST /Pacientes/Delete/{id} — Elimina el paciente con el id indicado de la BD.
    /// Si no existe, igual guarda cambios sin error.
    /// </summary>
    /// <param name="id">Identificador del paciente a eliminar.</param>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var paciente = await _context.Pacientes.FindAsync(id);
        if (paciente != null) _context.Pacientes.Remove(paciente);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}