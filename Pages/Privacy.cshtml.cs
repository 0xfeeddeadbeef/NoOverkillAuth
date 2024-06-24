using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NoOverkillAuth.Pages;

public class PrivacyModel : PageModel
{
    private readonly ILogger<PrivacyModel> _logger;

    [BindProperty]
    public string? UserName { get; set; }

    public PrivacyModel(ILogger<PrivacyModel> logger)
    {
        ArgumentNullException.ThrowIfNull(logger);
        _logger = logger;
    }

    public void OnGet()
    {
        _logger.LogDebug("HTTP GET /Privacy");

        this.UserName = this.User.Identity?.Name ?? "(NULL)";
    }
}
