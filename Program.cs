namespace NoOverkillAuth;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.CookiePolicy;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.Configure<CookiePolicyOptions>(static x =>
        {
            x.CheckConsentNeeded = static _ => false;
            x.HttpOnly = HttpOnlyPolicy.Always;
            x.MinimumSameSitePolicy = SameSiteMode.Strict;
            x.Secure = CookieSecurePolicy.Always;
        });

        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(static x =>
            {
                x.LoginPath = new PathString("/");
            });

        builder.Services.AddAuthorizationBuilder()
            .AddPolicy("SecondFactorPolicy",
                static x => x.RequireClaim(JwtClaimTypes.AuthenticationMethod, AuthenticationMethods.MultiFactorAuthentication));

        builder.Services.AddRazorPages(x =>
        {
            x.Conventions.AuthorizePage("/Privacy");
            x.Conventions.AuthorizePage("/TwoFactorLogin");
            x.Conventions.AuthorizePage("/TwoFactor", policy: "SecondFactorPolicy");
        });

        var app = builder.Build();

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapRazorPages();
        app.Run();
    }
}
