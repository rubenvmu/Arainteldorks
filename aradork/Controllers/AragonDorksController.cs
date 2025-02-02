using aradork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace aradork.Controllers
{
    public class AragonDorksController : Controller
    {
        private readonly aradorkDBContext _context;

        public AragonDorksController(aradorkDBContext context)
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

    // Verificar si 'id' tiene valor y luego convertirlo a cadena si es necesario
    if (id.HasValue)
    {
        aragonDorks = aragonDorks.Where(d => d.Id != null && d.Id.Contains(id.ToString()));  // Puede haber una advertencia de referencia nula aquí
    }

    // Asegurar que 'searchString' no sea nulo o vacío antes de hacer la búsqueda
    if (!string.IsNullOrEmpty(searchString))
    {
        aragonDorks = aragonDorks.Where(d => d.DorkValue != null && d.DorkValue.Contains(searchString));  // Aquí también puede haber una advertencia
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
        public async Task<IActionResult> Create([Bind("Id,DorkValue,Nombre,Descripcion")] AragonDorks aragonDorks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aragonDorks); // Corregido el nombre de la variable a 'aragonDorks'
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aragonDorks);  // Usamos 'aragonDorks' en lugar de 'aragonDork'
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,DorkValue,Nombre,Descripcion")] AragonDorks aragonDorks)
        {
            if (id != aragonDorks.Id) // Corregido el nombre de la variable
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aragonDorks);  // Corregido el nombre de la variable
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AragonDorkExists(aragonDorks.Id)) // Corregido el nombre de la variable
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
            return View(aragonDorks);  // Usamos 'aragonDorks' en lugar de 'aragonDork'
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