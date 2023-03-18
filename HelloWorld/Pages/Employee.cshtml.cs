using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HelloWorld.Data;

namespace HelloWorld.Pages;

class EmployeeModel : PageModel{

    private readonly TimecardContext _context;
    public Timecard entry {get; private set;}
    public EmployeeModel(TimecardContext context) {
        _context = context;
    }

    public async Task OnGetAsync(){
        IList<Timecard> card = await _context.Timecard
            .ToListAsync();
        entry = card.ElementAt(0);
        //return card.ElementAt(0);
    }
}