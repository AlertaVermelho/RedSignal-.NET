using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using RedSignal.Data;
using RedSignal.Models;

namespace RedSignal.Pages.Admin.MonitoredLocationsManager;

[Authorize]
public class EditModel : PageModel
{
    private readonly AppDbContext _context;

    public EditModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public LocalMonitorado Local { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(long id)
    {
        Local = await _context.LocaisMonitorados.FindAsync(id);
        if (Local == null) return NotFound();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var existing = await _context.LocaisMonitorados.FindAsync(Local.Id);
        if (existing == null) return NotFound();

        existing.NomeLocal = Local.NomeLocal;
        existing.Latitude = Local.Latitude;
        existing.Longitude = Local.Longitude;
        existing.RaioNotificacaoKm = Local.RaioNotificacaoKm;
        existing.DataAtualizacao = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
