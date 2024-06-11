using Araintelsoftware.Models;

namespace Araintelsoftware.Services.Search
{
    public interface IBuscadorAragondorks
    {
        Task<IEnumerable<AragonDork>> GetDorks(string getId, string searchDork);
    }
}
