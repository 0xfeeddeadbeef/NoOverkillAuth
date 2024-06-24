namespace NoOverkillAuth.Pages;

using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        ArgumentNullException.ThrowIfNull(logger);
        _logger = logger;
    }

    [BindProperty]
    public string? UserName { get; set; }

    [BindProperty, DataType(DataType.Password)]
    public string? Password { get; set; }

    public string? Message { get; set; }

    public void OnGet()
    {
        _logger.LogDebug("HTTP GET /Index");
    }

    public async Task<IActionResult> OnPost()
    {
        if (string.Equals(this.UserName, "admin", StringComparison.OrdinalIgnoreCase) &&
            string.Equals(this.Password, "123", StringComparison.Ordinal))
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, "admin")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await this.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToPage("/Privacy");
        }
        else
        {
            this.Message = "Invalid attempt!";
        }

        return Page();
    }
}
