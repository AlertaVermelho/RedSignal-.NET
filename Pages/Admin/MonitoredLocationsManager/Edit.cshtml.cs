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
        Local = await _context.LocaisMonitorados.FindAsync(id); // id is the PK, so it maps to id_local_monitorado
        if (Local == null) return NotFound();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        // Local.id_local_monitorado is bound from the form
        var existing = await _context.LocaisMonitorados.FindAsync(Local.id_local_monitorado);
        if (existing == null) return NotFound();

        existing.nome_local = Local.nome_local;
        existing.latitude = Local.latitude;
        existing.longitude = Local.longitude;
        existing.raio_notificacao_km = Local.raio_notificacao_km;
        existing.data_atualizacao = DateTime.UtcNow;
        // existing.id_usuario_registrou should not be changed here, it's set on creation.

        await _context.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
