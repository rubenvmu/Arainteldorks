using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Araintelsoftware.Models;

namespace Araintelsoftware.Controllers
{
    public class AgendaController : Controller
    {
        private readonly AraintelsqlContext _context;

        public AgendaController(AraintelsqlContext context)
        {
            _context = context;
        }

        // GET: Agenda
        public async Task<IActionResult> Index()
        {
            return View(await _context.Agenda.ToListAsync());
        }

        // GET: Agenda/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendum = await _context.Agenda
                .FirstOrDefaultAsync(m => m.Firstname == id);
            if (agendum == null)
            {
                return NotFound();
            }

            return View(agendum);
        }

        // GET: Agenda/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Agenda/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Firstname,Lastname,Url,Mail,Company,Postion,LastConnection")] Agendum agendum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agendum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(agendum);
        }

        // GET: Agenda/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendum = await _context.Agenda.FindAsync(id);
            if (agendum == null)
            {
                return NotFound();
            }
            return View(agendum);
        }


        // POST: Agenda/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Firstname,Lastname,Url,Mail,Company,Postion,LastConnection")] Agendum agendum)
        {
            if (id != agendum.Firstname)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agendum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgendumExists(agendum.Firstname))
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
            return View(agendum);
        }

        // GET: Agenda/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendum = await _context.Agenda
                .FirstOrDefaultAsync(m => m.Firstname == id);
            if (agendum == null)
            {
                return NotFound();
            }

            return View(agendum);
        }

        // POST: Agenda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var agendum = await _context.Agenda.FindAsync(id);
            if (agendum != null)
            {
                _context.Agenda.Remove(agendum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgendumExists(string id)
        {
            return _context.Agenda.Any(e => e.Firstname == id);
        }
    }
}
