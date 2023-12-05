using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Air2.Data;
using Air2.Models;

namespace Air2.Controllers
{
    public class TripulacionsController : Controller
    {
        private readonly Air2Context _context;

        public TripulacionsController(Air2Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            if (_context.Tripulacion == null)
            {
                return Problem("Entity set'Proto1_AereaCOMPContext'is null");
            }

            var Tripulacions = from Tripulacion in _context.Tripulacion
                               select Tripulacion;
            if (!String.IsNullOrEmpty(searchString))
            {
                Tripulacions = Tripulacions.Where(s => s.Nombre!.Contains(searchString));
            }
            return View(await Tripulacions.ToListAsync());
        }

        // GET: Tripulacions
       // public async Task<IActionResult> Index()
        //{
              //return _context.Tripulacion != null ? 
                         // View(await _context.Tripulacion.ToListAsync()) :
                          //Problem("Entity set 'Air2Context.Tripulacion'  is null.");
       // }

        // GET: Tripulacions/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Tripulacion == null)
            {
                return NotFound();
            }

            var tripulacion = await _context.Tripulacion
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (tripulacion == null)
            {
                return NotFound();
            }

            return View(tripulacion);
        }

        // GET: Tripulacions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tripulacions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Nombre,Base")] Tripulacion tripulacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tripulacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tripulacion);
        }

        // GET: Tripulacions/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Tripulacion == null)
            {
                return NotFound();
            }

            var tripulacion = await _context.Tripulacion.FindAsync(id);
            if (tripulacion == null)
            {
                return NotFound();
            }
            return View(tripulacion);
        }

        // POST: Tripulacions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Codigo,Nombre,Base")] Tripulacion tripulacion)
        {
            if (id != tripulacion.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tripulacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripulacionExists(tripulacion.Codigo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tripulacion);
        }

        // GET: Tripulacions/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Tripulacion == null)
            {
                return NotFound();
            }

            var tripulacion = await _context.Tripulacion
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (tripulacion == null)
            {
                return NotFound();
            }

            return View(tripulacion);
        }

        // POST: Tripulacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Tripulacion == null)
            {
                return Problem("Entity set 'Air2Context.Tripulacion'  is null.");
            }
            var tripulacion = await _context.Tripulacion.FindAsync(id);
            if (tripulacion != null)
            {
                _context.Tripulacion.Remove(tripulacion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TripulacionExists(string id)
        {
          return (_context.Tripulacion?.Any(e => e.Codigo == id)).GetValueOrDefault();
        }
    }
}
