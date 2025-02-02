using aradork.Models;
using Microsoft.EntityFrameworkCore;

namespace aradork.Services.Search
{
    public class BuscadorAragondorks : IBuscadorAragondorks
    {
        private readonly aradorkDBContext _context;

        public BuscadorAragondorks(aradorkDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AragonDorks>> GetDorks(string getId, string searchDork)
        {
            var aragonDorks = from m in _context.AragonDorks
                              select m;

            // Verificar que 'getId' no sea nulo ni vacío antes de realizar la búsqueda.
            if (!string.IsNullOrEmpty(getId))
            {
                aragonDorks = aragonDorks.Where(s => s.Id != null && s.Id.Contains(getId));
            }

            // Verificar que 'searchDork' no sea nulo ni vacío antes de realizar la búsqueda.
            if (!string.IsNullOrEmpty(searchDork))
            {
                aragonDorks = aragonDorks.Where(s => s.DorkValue != null && s.DorkValue.Contains(searchDork));
            }

            return await aragonDorks.ToListAsync();
        }
    }
}