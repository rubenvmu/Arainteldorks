using aradork.Models;
using Microsoft.EntityFrameworkCore;

namespace aradork.Services
{
    public class DatabaseSeeder
    {
        private readonly aradorkDBContext _context;

        public DatabaseSeeder(aradorkDBContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (!_context.AragonDorks.Any()) // Verifica si la tabla está vacía
            {
                var dorks = new List<AragonDorks>
                {
                    new AragonDorks { Id = Guid.NewGuid().ToString(), DorkValue = "site:github.com password", Nombre = "GitHub Passwords", Descripcion = "Busca contraseñas expuestas en GitHub" },
                    new AragonDorks { Id = Guid.NewGuid().ToString(), DorkValue = "intitle:'index of' passwd", Nombre = "Index of Passwd", Descripcion = "Archivos passwd indexados" },
                    new AragonDorks { Id = Guid.NewGuid().ToString(), DorkValue = "filetype:sql 'password'", Nombre = "SQL Passwords", Descripcion = "Busca bases de datos SQL con credenciales" },
                    new AragonDorks { Id = Guid.NewGuid().ToString(), DorkValue = "inurl:'/wp-content/uploads/'", Nombre = "WordPress Uploads", Descripcion = "Archivos subidos en WordPress" }
                };

                _context.AragonDorks.AddRange(dorks);
                _context.SaveChanges();
            }
        }
    }
}