using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HelloWorld.Data;
using HelloWorld.Models;

namespace HelloWorld.Pages;

class EmployeeModel2 : PageModel
{

    private TimecardContext _context;
    public TimeRecord entry { get; private set; }
    public EmployeeModel2(TimecardContext context)
    {
        _context = context;
    }
    [BindProperty]
    public tester tester { get; set; } = default!;
    public void OnGet()
    {
        // Create a new employee and set its properties
        tester = new tester
        {
            numba = 2,
            text = "abc"
        };
        
        if (!ModelState.IsValid) {
            Console.WriteLine("oi\n");
        }
        // Add the new employee to the context and save changes
        _context.tester.Add(tester);
        _context.SaveChanges();
    }

}