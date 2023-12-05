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
    public class PilotoesController : Controller
    {
        private readonly Air2Context _context;

        public PilotoesController(Air2Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string searchString)
        {
            if (_context.Piloto == null)
            {
                return Problem("Entity set'Proto1_AereaCOMPContext'is null");
            }
            var Pilotoes = from Piloto in _context.Piloto
                           select Piloto;
            if (!String.IsNullOrEmpty(searchString))
            {
                Pilotoes = Pilotoes.Where(s => s.Base!.Contains(searchString));
            }
            return View(await Pilotoes.ToListAsync());

        }

        // GET: Pilotoes
        //public async Task<IActionResult> Index()
        //{
              //return _context.Piloto != null ? 
                         // View(await _context.Piloto.ToListAsync()) :
                         // Problem("Entity set 'Air2Context.Piloto'  is null.");
        //}

        // GET: Pilotoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Piloto == null)
            {
                return NotFound();
            }

            var piloto = await _context.Piloto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (piloto == null)
            {
                return NotFound();
            }

            return View(piloto);
        }

        // GET: Pilotoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pilotoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HorasVuelo,Nombre,Base")] Piloto piloto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(piloto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(piloto);
        }

        // GET: Pilotoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Piloto == null)
            {
                return NotFound();
            }

            var piloto = await _context.Piloto.FindAsync(id);
            if (piloto == null)
            {
                return NotFound();
            }
            return View(piloto);
        }

        // POST: Pilotoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HorasVuelo,Nombre,Base")] Piloto piloto)
        {
            if (id != piloto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(piloto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PilotoExists(piloto.Id))
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
            return View(piloto);
        }

        // GET: Pilotoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Piloto == null)
            {
                return NotFound();
            }

            var piloto = await _context.Piloto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (piloto == null)
            {
                return NotFound();
            }

            return View(piloto);
        }

        // POST: Pilotoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Piloto == null)
            {
                return Problem("Entity set 'Air2Context.Piloto'  is null.");
            }
            var piloto = await _context.Piloto.FindAsync(id);
            if (piloto != null)
            {
                _context.Piloto.Remove(piloto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PilotoExists(int id)
        {
          return (_context.Piloto?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
