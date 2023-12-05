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
    public class VueloesController : Controller
    {
        private readonly Air2Context _context;

        public VueloesController(Air2Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string searchString)
        {
            if (_context.Vuelo == null)
            {
                return Problem("Entity set'Proto1_AereaCOMPContext'is null");
            }
            var Vueloes = from Vuelo in _context.Vuelo
                          select Vuelo;
            if (!String.IsNullOrEmpty(searchString))
            {
                Vueloes = Vueloes.Where(s => s.Destino!.Contains(searchString));
            }
            return View(await Vueloes.ToListAsync());

        }

        // GET: Vueloes
       // public async Task<IActionResult> Index()
        //{
              //return _context.Vuelo != null ? 
                          //View(await _context.Vuelo.ToListAsync()) :
                          //Problem("Entity set 'Air2Context.Vuelo'  is null.");
        //}

        // GET: Vueloes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Vuelo == null)
            {
                return NotFound();
            }

            var vuelo = await _context.Vuelo
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (vuelo == null)
            {
                return NotFound();
            }

            return View(vuelo);
        }

        // GET: Vueloes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vueloes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Origen,Destino,HoraVuelo,BaseMantenimiento")] Vuelo vuelo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vuelo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vuelo);
        }

        // GET: Vueloes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Vuelo == null)
            {
                return NotFound();
            }

            var vuelo = await _context.Vuelo.FindAsync(id);
            if (vuelo == null)
            {
                return NotFound();
            }
            return View(vuelo);
        }

        // POST: Vueloes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Codigo,Origen,Destino,HoraVuelo,BaseMantenimiento")] Vuelo vuelo)
        {
            if (id != vuelo.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vuelo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VueloExists(vuelo.Codigo))
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
            return View(vuelo);
        }

        // GET: Vueloes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Vuelo == null)
            {
                return NotFound();
            }

            var vuelo = await _context.Vuelo
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (vuelo == null)
            {
                return NotFound();
            }

            return View(vuelo);
        }

        // POST: Vueloes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Vuelo == null)
            {
                return Problem("Entity set 'Air2Context.Vuelo'  is null.");
            }
            var vuelo = await _context.Vuelo.FindAsync(id);
            if (vuelo != null)
            {
                _context.Vuelo.Remove(vuelo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VueloExists(string id)
        {
          return (_context.Vuelo?.Any(e => e.Codigo == id)).GetValueOrDefault();
        }
    }
}
