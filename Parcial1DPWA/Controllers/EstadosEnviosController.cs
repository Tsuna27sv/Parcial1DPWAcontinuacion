using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Parcial1DPWA.Data;
using Parcial1DPWA.Models;

namespace Parcial1DPWA.Controllers
{
    public class EstadosEnviosController : Controller
    {
        private readonly AppDbContext _context;

        public EstadosEnviosController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.EstadosEnvios.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var estadosEnvio = await _context.EstadosEnvios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estadosEnvio == null) return NotFound();

            return View(estadosEnvio);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreEstado,Descripcion")] EstadosEnvio estadosEnvio)
        {
            try
            {
                _context.Add(estadosEnvio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al guardar el estado: " + ex.Message);
            }
            return View(estadosEnvio);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var estadosEnvio = await _context.EstadosEnvios.FindAsync(id);
            if (estadosEnvio == null) return NotFound();
            return View(estadosEnvio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreEstado,Descripcion")] EstadosEnvio estadosEnvio)
        {
            if (id != estadosEnvio.Id) return NotFound();

            try
            {
                _context.Update(estadosEnvio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                if (!EstadosEnvioExists(estadosEnvio.Id)) return NotFound();
                else throw;
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var estadosEnvio = await _context.EstadosEnvios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estadosEnvio == null) return NotFound();

            return View(estadosEnvio);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estadosEnvio = await _context.EstadosEnvios.FindAsync(id);
            if (estadosEnvio != null)
            {
                _context.EstadosEnvios.Remove(estadosEnvio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstadosEnvioExists(int id)
        {
            return _context.EstadosEnvios.Any(e => e.Id == id);
        }
    }
}