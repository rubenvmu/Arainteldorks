using aradork.Models;

namespace Araintelsoft.Services.Search
{
    public interface IBuscadorLinkedinService
    {
        Task<IEnumerable<Agendum>> GetContactos(string searchFirstname, string searchLastname, string searchCompany);
    }
}