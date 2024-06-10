using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Araintelsoftware.Areas.Identity.Data;

public class DashboardModel : PageModel
{
    private readonly UserManager<SampleUser> _userManager;

    public DashboardModel(UserManager<SampleUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            throw new InvalidOperationException("User is not logged in.");
        }
    }
}