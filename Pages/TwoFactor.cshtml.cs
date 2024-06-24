using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NoOverkillAuth.Pages;

public class TwoFactorModel : PageModel
{
    public string? UserName { get; set; }

    public void OnGet()
    {
        this.UserName = this.User.Identity?.Name ?? "(NULL)";
    }
}
