using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using RedSignal.Data;
using RedSignal.Models;

namespace RedSignal.Pages.Admin.MonitoredLocationsManager;

[Authorize]
public class DetailsModel : PageModel
{
    private readonly AppDbContext _context;

    public DetailsModel(AppDbContext context)
    {
        _context = context;
    }

    public LocalMonitorado? Local { get; set; }

    public async Task<IActionResult> OnGetAsync(long id)
    {
        Local = await _context.LocaisMonitorados.FindAsync(id);

        if (Local == null)
            return NotFound();

        return Page();
    }
}
