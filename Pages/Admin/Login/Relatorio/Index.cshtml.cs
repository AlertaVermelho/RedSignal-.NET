using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RedSignal.Data;
using RedSignal.Models;

namespace RedSignal.Pages.Admin.Relatorio;

[Authorize]
public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    public List<LocalMonitorado> Locais { get; set; } = new();

    public async Task OnGetAsync()
    {
        Locais = await _context.LocaisMonitorados
            .OrderByDescending(l => l.DataCriacao)
            .ToListAsync();
    }
}
