using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWorld.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HelloWorld.Pages
{
    public class ManageModel : PageModel
    {
        private readonly TimecardContext _context;
        public ManageModel(TimecardContext context)
        {
            _context = context;
        }

        public IList<Employee> StarredEmployees { get; set; }
        public IList<Employee> WorkingEmployees { get; set; }
        public IList<Employee> AllEmployees { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (TempData.ContainsKey("ETitle") && (string?)TempData.Peek("ETitle") != "Manager")
            {
                TempData["Message"] = "You are no manager >:I";
                return RedirectToPage("/index");
            }

            var managerId = (int)TempData.Peek("EId");
            var twoWeeksAgo = DateTime.Today.AddDays(-14);

            // Employees with a starred time record in the last two weeks
            StarredEmployees = await _context.Employees
                .Where(e => e.ManagerId == managerId && e.TimeRecords.Any(tr => tr.IsStarred == 1 && tr.TimeIn >= twoWeeksAgo))
                .ToListAsync();

            // Currently working employees
            WorkingEmployees = await _context.Employees
                .Where(e => e.ManagerId == managerId && e.TimeRecords.Any(tr => tr.TimeOut == null))
                .ToListAsync();

            // Employees with time records in the last two weeks
            AllEmployees = await _context.Employees
                .Where(e => e.ManagerId == managerId && e.TimeRecords.Any(tr => tr.TimeIn >= twoWeeksAgo))
                .ToListAsync();

            return Page();
        }
    }

}