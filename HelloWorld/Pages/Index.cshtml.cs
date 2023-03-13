using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HelloWorld.Data;

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
    public IList<Timecard> card {get; set; }

    public async Task OnGetAsync()
    {
        card = await _context.Timecard
            //.Include(_ => _.timeId)
            //.Include(_ => _.timeIn)
            //.AsNoTracking()
            .ToListAsync();
        foreach (var t in card) {
            Console.WriteLine(t.empId);
            Console.WriteLine(t.timeId);
            Console.WriteLine(t.timeIn);
            Console.WriteLine(t.timeOut);
        }
    }
}
