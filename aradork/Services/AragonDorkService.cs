using aradork.Models;
using Microsoft.EntityFrameworkCore;

namespace aradork.Services
{
    public class AragonDorkService
    {
        private readonly aradorkDBContext _context;

        public AragonDorkService(aradorkDBContext context)
        {
            _context = context;
        }

        public async Task<List<AragonDorks>> SearchDorks(string searchTerm)
{
    return await _context.AragonDorks
        .Where(d => d.Nombre.Contains(searchTerm) || 
                   d.DorkValue.Contains(searchTerm) || 
                   d.Descripcion.Contains(searchTerm))
        .ToListAsync();
}

        public async Task<List<AragonDorks>> GetAllDorks()
        {
            return await _context.AragonDorks.ToListAsync();
        }

        public async Task<AragonDorks?> GetDorkById(string id)
        {
            return await _context.AragonDorks.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task AddDork(AragonDorks newDork)
{
    // Asegurar que el ID no sea nulo
    if (string.IsNullOrEmpty(newDork.Id))
    {
        newDork.Id = Guid.NewGuid().ToString();
    }
    
    _context.AragonDorks.Add(newDork);
    await _context.SaveChangesAsync();
}

        public async Task<bool> UpdateDork(AragonDorks updatedDork)
        {
            var existingDork = await _context.AragonDorks.FindAsync(updatedDork.Id);
            if (existingDork == null)
                return false;

            existingDork.DorkValue = updatedDork.DorkValue;
            existingDork.Nombre = updatedDork.Nombre;
            existingDork.Descripcion = updatedDork.Descripcion;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDork(string id)
        {
            var dork = await _context.AragonDorks.FindAsync(id);
            if (dork == null)
                return false;

            _context.AragonDorks.Remove(dork);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}