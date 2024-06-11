using System.Threading.Tasks;
using Araintelsoftware.Models;

namespace Araintelsoftware.Services.Search
{
    public interface IBuscadorAragondorks
    {
        Task<IEnumerable<AragonDork>> GetDorks(string getId, string searchDork);
    }
}
