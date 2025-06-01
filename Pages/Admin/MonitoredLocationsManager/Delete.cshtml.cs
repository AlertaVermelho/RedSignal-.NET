using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using RedSignal.Data;
using RedSignal.Models;

namespace RedSignal.Pages.Admin.MonitoredLocationsManager;

[Authorize]
public class DeleteModel : PageModel
{
    private readonly AppDbContext _context;

    public DeleteModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public LocalMonitorado? Local { get; set; }

    public async Task<IActionResult> OnGetAsync(long id)
    {
        Local = await _context.LocaisMonitorados.FindAsync(id);
        if (Local == null) return NotFound();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(long id)
    {
        var local = await _context.LocaisMonitorados.FindAsync(id);
        if (local == null) return NotFound();

        _context.LocaisMonitorados.Remove(local);
        await _context.SaveChangesAsync();

        return RedirectToPage("Index");
    }
}
