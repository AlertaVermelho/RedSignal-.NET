using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using RedSignal.Data;
using RedSignal.Models;

namespace RedSignal.Pages.Admin.MonitoredLocationsManager;

[Authorize]
public class CreateModel : PageModel
{
    private readonly AppDbContext _context;

    public CreateModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public LocalMonitorado Local { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        Local.DataCriacao = DateTime.UtcNow;
        Local.DataAtualizacao = DateTime.UtcNow;

        _context.LocaisMonitorados.Add(Local);
        await _context.SaveChangesAsync();

        return RedirectToPage("Index");
    }
}
