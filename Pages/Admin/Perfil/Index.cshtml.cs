using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RedSignal.Pages.Admin.Perfil;

[Authorize]
public class IndexModel : PageModel
{
    public void OnGet()
    {
    }
}
