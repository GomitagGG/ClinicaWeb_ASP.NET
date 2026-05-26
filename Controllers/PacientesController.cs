using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClinicaWeb.Data;
using ClinicaWeb.Models;

namespace ClinicaWeb.Controllers;

public class PacientesController : Controller
{
    private readonly ClinicaContext _context;

    public PacientesController(ClinicaContext context)
    {
        _context = context;
    }

    // INDEX
    public async Task<IActionResult> Index()
    {
        return View(await _context.Pacientes.ToListAsync());
    }

    // CREATE POST
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

    // EDIT POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Paciente paciente)
    {
        if (id != paciente.Id) return NotFound();
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

    // DELETE POST
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