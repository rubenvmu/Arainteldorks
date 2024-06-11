using Araintelsoftware.Data;
using Araintelsoftware.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IActionResult> searchDorks(int? id, string searchString)
        {
            ViewData["CurrentId"] = id;
            ViewData["CurrentFilter"] = searchString;

            var aragonDorks = from d in _context.AragonDorks
                              select d;

            if (id.HasValue)
            {
                aragonDorks = aragonDorks.Where(d => d.Id.ToString().Contains(id.ToString()));
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                aragonDorks = aragonDorks.Where(d => d.DorkValue.Contains(searchString));
            }

            return View("Index", await aragonDorks.ToListAsync());
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
