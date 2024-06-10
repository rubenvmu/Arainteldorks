using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Araintelsoftware.Data;
using Araintelsoftware.Models;

namespace Araintelsoftware.Controllers
{
    public class AragonDorksController : Controller
    {
        private readonly AragonDorksContext _context;

        public AragonDorksController(AragonDorksContext context)
        {
            _context = context;
        }

        // GET: AragonDorks
        public async Task<IActionResult> Index()
        {
            return View(await _context.AragonDorks.ToListAsync());
        }

        // GET: AragonDorks/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aragonDork = await _context.AragonDorks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aragonDork == null)
            {
                return NotFound();
            }

            return View(aragonDork);
        }

        // GET: AragonDorks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AragonDorks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DorkValue")] AragonDork aragonDork)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aragonDork);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aragonDork);
        }

        // GET: AragonDorks/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aragonDork = await _context.AragonDorks.FindAsync(id);
            if (aragonDork == null)
            {
                return NotFound();
            }
            return View(aragonDork);
        }

        // POST: AragonDorks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,DorkValue")] AragonDork aragonDork)
        {
            if (id != aragonDork.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aragonDork);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AragonDorkExists(aragonDork.Id))
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
            return View(aragonDork);
        }

        // GET: AragonDorks/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aragonDork = await _context.AragonDorks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aragonDork == null)
            {
                return NotFound();
            }

            return View(aragonDork);
        }

        // POST: AragonDorks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var aragonDork = await _context.AragonDorks.FindAsync(id);
            if (aragonDork != null)
            {
                _context.AragonDorks.Remove(aragonDork);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AragonDorkExists(string id)
        {
            return _context.AragonDorks.Any(e => e.Id == id);
        }
    }
}
