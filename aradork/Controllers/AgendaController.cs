using Araintelsoft.Services.Search;
using aradork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace aradork.Controllers
{
    public class AgendaController : Controller
    {
        private readonly aradorkDBContext _context;
        private readonly IConfiguration _configuration;

        // Constructor con inyección de dependencias
        public AgendaController(aradorkDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: Agenda
        public async Task<IActionResult> Index()
        {
            var agenda = await _context.Agenda.ToListAsync();
            return View(agenda);
        }

        // Función de búsqueda en la agenda
        public async Task<IActionResult> Search(string searchFirstname, string searchLastname, string searchCompany)
        {
            var buscadorService = new BuscadorService(_context);
            var contactos = await buscadorService.GetContactos(searchFirstname, searchLastname, searchCompany);
            return View("Search", contactos);
        }

        // GET: Agenda/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
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

        // POST: Agenda/ValidatePassword
        [HttpPost]
        public IActionResult ValidatePassword(string password)
        {
            var agendaSecretKey = _configuration["AgendaSecretKey"];
            bool isValid = password == agendaSecretKey;

            if (isValid)
            {
                HttpContext.Session.SetString("IsValidPassword", "true");
            }

            return Json(new { isValid });
        }

        // GET: Agenda/_TablePartial
        [HttpGet]
        public IActionResult _TablePartial()
        {
            if (HttpContext.Session.GetString("IsValidPassword") == "true")
            {
                var model = _context.Agenda.ToList();
                return PartialView("_TablePartial", model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Agenda/Create
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
            if (string.IsNullOrEmpty(id))
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
            if (string.IsNullOrEmpty(id))
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
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // Método privado para verificar si el agendum existe en la base de datos
        private bool AgendumExists(string id)
        {
            return _context.Agenda.Any(e => e.Firstname == id);
        }
    }
}