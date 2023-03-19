using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HelloWorld.Data;
using HelloWorld.Models;

namespace HelloWorld.Pages;

class EmployeeModel2 : PageModel{

    private readonly TimecardContext _context;
    public TimeRecord entry {get; private set;}
    public EmployeeModel2(TimecardContext context) {
        _context = context;
    }

    public async Task OnGetAsync(){
        IList<TimeRecord> card = await _context.TimeRecord
            .ToListAsync();
        entry = card.ElementAt(0);
        //return card.ElementAt(0);
    }
}