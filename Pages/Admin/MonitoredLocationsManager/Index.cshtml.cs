using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using RedSignal.Data;
using RedSignal.Models;

namespace RedSignal.Pages.Admin.MonitoredLocationsManager;

[Authorize]
public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    public List<LocalMonitorado> Locais { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public long? UserIdFilter { get; set; }

    public async Task OnGetAsync()
    {
        var query = _context.LocaisMonitorados.AsQueryable();

        if (UserIdFilter.HasValue)
        {
            query = query.Where(l => l.UserId == UserIdFilter.Value);
        }

        Locais = await query.OrderByDescending(l => l.DataCriacao).ToListAsync();
    }
}
