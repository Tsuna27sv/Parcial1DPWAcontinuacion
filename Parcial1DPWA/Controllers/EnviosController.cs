using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Parcial1DPWA.Data;
using Parcial1DPWA.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Parcial1DPWA.Controllers
{
    [Authorize]
    public class EnviosController : Controller
    {
        private readonly AppDbContext _context;

        public EnviosController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IQueryable<Envio> enviosQuery = _context.Envios
                .Include(e => e.Cliente)
                .Include(e => e.Destinatario)
                .Include(e => e.EstadosEnvio)
                .Include(e => e.Paquete);

            if (!User.IsInRole("Administrador"))
            {
                var userEmail = User.Identity.Name;
                enviosQuery = enviosQuery.Where(e => e.Cliente.Nombre == userEmail);
            }

            return View(await enviosQuery.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var envio = await _context.Envios
                .Include(e => e.Cliente)
                .Include(e => e.Destinatario)
                .Include(e => e.EstadosEnvio)
                .Include(e => e.Paquete)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (envio == null) return NotFound();

            if (!User.IsInRole("Administrador") && envio.Cliente.Nombre != User.Identity.Name)
            {
                return Forbid();
            }

            return View(envio);
        }

        public IActionResult Create()
        {
            CargarListas();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClienteId,DestinatarioId,PaqueteId,EstadosEnvioId,DepartamentoDestino,MontoTotal")] Envio envio)
        {
            try
            {
                // 1. Asignación automática de fecha
                envio.FechaCreacion = DateTime.Now;

                // 2. Si es un Usuario normal, asignamos su ID automáticamente
                if (!User.IsInRole("Administrador"))
                {
                    var nombreUsuario = User.Identity.Name;
                    var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Nombre == nombreUsuario);
                    if (cliente != null)
                    {
                        envio.ClienteId = cliente.Id;
                    }
                    envio.EstadosEnvioId = 1; // Forzar "Pendiente" para clientes
                }

                // 3. Guardar en la base de datos
                _context.Add(envio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Si hay un error de base de datos (como el que viste en rojo), se mostrará aquí
                ModelState.AddModelError("", "Error al guardar: " + ex.InnerException?.Message ?? ex.Message);
            }

            CargarListas(envio);
            return View(envio);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var envio = await _context.Envios.Include(e => e.EstadosEnvio).FirstOrDefaultAsync(e => e.Id == id);
            if (envio == null) return NotFound();

            if (!User.IsInRole("Administrador") && envio.EstadosEnvioId != 1)
            {
                return RedirectToAction("Index");
            }

            CargarListas(envio);
            return View(envio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClienteId,DestinatarioId,PaqueteId,EstadosEnvioId,FechaCreacion,DepartamentoDestino,MontoTotal")] Envio envio)
        {
            if (id != envio.Id) return NotFound();

            var envioDb = await _context.Envios.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);

            if (!User.IsInRole("Administrador") && envioDb.EstadosEnvioId != 1)
            {
                return BadRequest("No se puede editar.");
            }

            try
            {
                _context.Update(envio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error: " + ex.Message);
            }

            CargarListas(envio);
            return View(envio);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var envio = await _context.Envios
                .Include(e => e.Cliente)
                .Include(e => e.EstadosEnvio)
                .Include(e => e.Paquete)
                .Include(e => e.Destinatario)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (envio == null) return NotFound();

            if (!User.IsInRole("Administrador") && envio.EstadosEnvioId != 1)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(envio);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var envio = await _context.Envios.FindAsync(id);
            if (envio == null) return NotFound();

            if (!User.IsInRole("Administrador") && envio.EstadosEnvioId != 1)
            {
                return BadRequest("No se puede eliminar.");
            }

            _context.Envios.Remove(envio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void CargarListas(Envio envio = null)
        {
            // Listas de Clientes y Estados
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre", envio?.ClienteId);

            if (!User.IsInRole("Administrador"))
            {
                ViewData["EstadosEnvioId"] = new SelectList(_context.EstadosEnvios.Where(e => e.Id == 1), "Id", "NombreEstado", 1);
            }
            else
            {
                ViewData["EstadosEnvioId"] = new SelectList(_context.EstadosEnvios, "Id", "NombreEstado", envio?.EstadosEnvioId);
            }

            ViewData["DestinatarioId"] = new SelectList(_context.Destinatarios, "Id", "Nombre", envio?.DestinatarioId);

            // CARGAR PAQUETES: Aseguramos que la lista se cargue siempre
            var listaPaquetes = _context.Paquetes.ToList();
            ViewData["PaqueteId"] = new SelectList(listaPaquetes, "Id", "Descripcion", envio?.PaqueteId);
        }

        private bool EnvioExists(int id)
        {
            return _context.Envios.Any(e => e.Id == id);
        }
    }
}