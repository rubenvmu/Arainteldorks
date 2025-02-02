using aradork.Models;

namespace aradork.Services.Search
{
    public interface IBuscadorAragondorks
    {
        Task<IEnumerable<AragonDorks>> GetDorks(string getId, string searchDork);
    }
}
