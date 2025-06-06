using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace RedSignal.Pages.Admin.Login;

public class LoginModel : PageModel
{
    private readonly IConfiguration _config;

    public LoginModel(IConfiguration config)
    {
        _config = config;
    }

    [BindProperty]
    public LoginInput Input { get; set; } = new();

    public string? ErrorMessage { get; set; }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        var expectedUser = _config["AdminLogin:Username"];
        var expectedPass = _config["AdminLogin:Password"];

        if (Input.Username == expectedUser && Input.Password == expectedPass)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, Input.Username) };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return RedirectToPage("/Admin/MonitoredLocationsManager/Index");
        }

        ErrorMessage = "Usuário ou senha inválidos.";
        return Page();
    }

    public class LoginInput
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
