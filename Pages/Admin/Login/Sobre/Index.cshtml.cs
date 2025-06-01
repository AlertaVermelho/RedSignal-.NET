using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RedSignal.Pages.Admin.Sobre;

[Authorize]
public class IndexModel : PageModel
{
    public void OnGet()
    {
    }
}
