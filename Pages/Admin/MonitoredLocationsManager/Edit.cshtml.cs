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
        var local = await _context.LocaisMonitorados.FindAsync(id);
        if (local == null) return NotFound();
        Local = local;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        Local.DataAtualizacao = DateTime.UtcNow;
        _context.Attach(Local).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.LocaisMonitorados.Any(e => e.Id == Local.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("Index");
    }
}
