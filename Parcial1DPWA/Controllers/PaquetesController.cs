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
    public class PaquetesController : Controller
    {
        private readonly AppDbContext _context;

        public PaquetesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Paquetes.Include(p => p.Envio);
            return View(await appDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var paquete = await _context.Paquetes
                .Include(p => p.Envio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paquete == null) return NotFound();

            return View(paquete);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DescripcionContenido,Peso")] Paquete paquete)
        {
            try
            {
                paquete.EnvioId = null;
                _context.Add(paquete);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var mensaje = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                ModelState.AddModelError("", "Error de base de datos: " + mensaje);
            }

            return View(paquete);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var paquete = await _context.Paquetes.FindAsync(id);
            if (paquete == null) return NotFound();

            return View(paquete);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EnvioId,DescripcionContenido,Peso")] Paquete paquete)
        {
            if (id != paquete.Id) return NotFound();

            try
            {
                _context.Update(paquete);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                if (!PaqueteExists(paquete.Id)) return NotFound();
                else throw;
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var paquete = await _context.Paquetes
                .Include(p => p.Envio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paquete == null) return NotFound();

            return View(paquete);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paquete = await _context.Paquetes.FindAsync(id);
            if (paquete != null)
            {
                _context.Paquetes.Remove(paquete);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaqueteExists(int id)
        {
            return _context.Paquetes.Any(e => e.Id == id);
        }
    }
}