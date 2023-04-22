using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TimecardApp.Models;

namespace TimecardApp.Pages{

    public class MyemployeesModel : PageModel{
        
        TimecardContext _context;
        public MyemployeesModel(TimecardContext context){
            _context = context;
        }
        public IList<Employee> employees {get; set;}
        public async Task<IActionResult> OnGetAsync(){
            employees = await _context.Employees
                .Where(e => e.ManagerId == (int)TempData.Peek("EId"))
                .ToListAsync();
            return Page();
        }
    }
}