using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using HelloWorld.Models;

namespace HelloWorld.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;
        private readonly TimecardContext _context;
        public LoginModel(ILogger<LoginModel> logger, TimecardContext context)
        {
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        [Required(ErrorMessage = "Please enter your Eid.")]
        public int Eid { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please enter your password.")]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // retrieve employee from database using Eid and password
            var employee = await RetrieveEmployeeAsync(Eid, Password);

            if (employee == null)
            {
                ErrorMessage = "Invalid Eid or password";
                return Page();
            }

            // store employee information in TempData
            TempData["EId"] = employee.EId;
            TempData["EFirstName"] = employee.EFirstName;
            TempData["ELastName"] = employee.ELastName;
            TempData["ETitle"] = employee.ETitle;
            TempData["ManagerId"] = employee.ManagerId;

            return RedirectToPage("/Index");
        }

        private async Task<Employee> RetrieveEmployeeAsync(int Eid, string password)
        {
            // Find the employee with the given Eid and password
            Employee employee = await _context.Employees.SingleOrDefaultAsync(e => e.EId == Eid && e.EPassword == password);

            // Return the employee if found, otherwise return null
            return employee;
        }

    }
}