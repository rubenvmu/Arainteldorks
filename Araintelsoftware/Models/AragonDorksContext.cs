// Araintelsoftware.Models

// Araintelsoftware.Data
using Araintelsoftware.Models;
using Microsoft.EntityFrameworkCore;

namespace Araintelsoftware.Data

{

    public class AragonDorksContext : DbContext

    {

        public AragonDorksContext(DbContextOptions<AragonDorksContext> options)

            : base(options)

        {

        }


        public DbSet<AragonDork> AragonDorks { get; set; }

    }

}