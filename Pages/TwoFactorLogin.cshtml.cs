namespace NoOverkillAuth.Pages;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class TwoFactorLoginModel : PageModel
{
    [BindProperty]
    public string? TotpCode { get; set; }

    public string? Message { get; set; }

    public string? UserName { get; set; }

    public void OnGet()
    {
        this.UserName = this.User.Identity?.Name ?? "(NULL)";
    }

    public async Task<IActionResult> OnPost()
    {
        if (string.Equals(this.TotpCode, "123", StringComparison.OrdinalIgnoreCase))
        {
            var claims = new List<Claim>
            {
                new(JwtClaimTypes.AuthenticationMethod, AuthenticationMethods.MultiFactorAuthentication)
            };

            this.User.AddIdentity(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

            await this.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, this.User);

            return RedirectToPage("/TwoFactor");
        }
        else
        {
            this.Message = "Invalid attempt!";
        }

        return Page();
    }
}
