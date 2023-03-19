using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HelloWorld.Data;
using HelloWorld.Models;

namespace HelloWorld.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly TimecardContext _context;

    public IndexModel(ILogger<IndexModel> logger, TimecardContext context)
    {
        _logger = logger;
        _context = context;
    }
    public IList<TimeRecord> card {get; set; }

    public async Task OnGetAsync()
    {
        card = await _context.TimeRecord
            //.Include(_ => _.timeId)
            //.Include(_ => _.timeIn)
            //.AsNoTracking()
            .ToListAsync();
        foreach (var t in card) {
            Console.WriteLine(t.eId);
            Console.WriteLine(t.timeId);
            Console.WriteLine(t.timeIn);
            Console.WriteLine(t.timeOut);
        }
    }
}
