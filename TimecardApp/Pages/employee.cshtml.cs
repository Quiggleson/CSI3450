using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TimecardApp.Models;

namespace TimecardApp.Pages
{
    public class EmployeeModel : PageModel
    {
        private readonly TimecardContext _context;

        public EmployeeModel(TimecardContext context)
        {
            _context = context;
        }

        public IList<TimeRecord> TimeRecords { get; set; }

        public async Task<IActionResult> OnGetAsync(int employeeid)
        {
            if (TempData.ContainsKey("ETitle") && (string)TempData.Peek("ETitle") != "Manager"){
                TempData["Message"] = "Access denied, you are not a manager";
                RedirectToPage("/index");
            }

            TimeRecords = await _context.TimeRecords
                .Where(t => t.EId == employeeid)
                .OrderBy(t => t.TimeIn)
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(DateTime newTimeIn, DateTime? newTimeOut, string submitEdit, string cancelEdit, int employeeId, string editableRecord, DateTime? oldTimeIn)
        {
            if (submitEdit != null && oldTimeIn != null)
            {

                try {
                    Console.WriteLine("###### right before the raw");
                    _context.Database.ExecuteSqlRaw("CALL UpdateTimeRecord({0}, {1}, {2}, {3})", employeeId, oldTimeIn, newTimeIn, newTimeOut);
                } catch (Exception e) {
                    TempData["Message"] = e.Message;
                    RedirectToPage();
                }
            }

            if (cancelEdit != null)
            {
                TempData.Remove("EditableRecord");
            }
            else
            {
                TempData["EditableRecord"] = editableRecord;
            }

            return RedirectToPage(new { employeeid = employeeId });
        }
    }
}
