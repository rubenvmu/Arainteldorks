using Araintelsoftware.Data;
using Araintelsoftware.Models;
using Microsoft.EntityFrameworkCore;

namespace Araintelsoftware.Services.Search
{
    public class BuscadorAragondorks : IBuscadorAragondorks
    {
        private readonly AragonDorksContext _context;

        public BuscadorAragondorks(AragonDorksContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AragonDork>> GetDorks(string getId, string searchDork)
        {
            var aragonDorks = from m in _context.AragonDorks
                              select m;

            if (!String.IsNullOrEmpty(getId))
            {
                aragonDorks = aragonDorks.Where(s => s.Id.Contains(getId));
            }

            if (!String.IsNullOrEmpty(searchDork))
            {
                aragonDorks = aragonDorks.Where(s => s.DorkValue.Contains(searchDork));
            }

            return await aragonDorks.ToListAsync();
        }
    }
}
