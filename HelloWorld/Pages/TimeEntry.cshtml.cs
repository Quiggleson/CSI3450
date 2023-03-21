using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HelloWorld.Data;
using HelloWorld.Models;
using System.Security.Claims;
using System;

public class TimeEntryModel : PageModel
{
    private readonly TimecardContext _context;

    public int eId { get; set; }
    public string eName { get; set; }
    public IList<TimeRecord> timerecord {get; set; }

    public TimeEntryModel(TimecardContext context)
    {
        _context = context;

    }

    public async Task<IActionResult> OnGetAsync()
    {
        eId = HttpContext.Session.GetInt32("eId")??-1;
        eName = HttpContext.Session.GetString("eName");
        timerecord = await _context.TimeRecord
            .Where(p => p.eId == eId)
            .ToListAsync();

        return Page();
    }

    public async  Task<IActionResult> OnPostAsync(string clockIn, string clockOut)
    {
        eId = HttpContext.Session.GetInt32("eId")??-1;
        eName = HttpContext.Session.GetString("eName");
        try
        {
            if (!string.IsNullOrEmpty(clockIn))
            {

                // create a new time entry for clocking in
                TimeRecord timeEntry = new TimeRecord
                {
                    eId = this.eId,
                    timeIn = DateTime.Now,

                };


                // add the new time entry to the database
                _context.TimeRecord.Add(timeEntry);
                await _context.SaveChangesAsync();
            }
            else if (!string.IsNullOrEmpty(clockOut))
            {
                // create a new time entry for clocking out
                var lastTimeEntry = _context.TimeRecord
                    .Where(te => te.eId == eId)
                    .OrderByDescending(te => te.timeIn)
                    .FirstOrDefault();

                if (lastTimeEntry != null)
                {
                    lastTimeEntry.timeOut = DateTime.Now;

                    // update the existing time entry in the database
                    _context.TimeRecord.Update(lastTimeEntry);
                    await _context.SaveChangesAsync();
                }
            }

            timerecord = await _context.TimeRecord
            .Where(p => p.eId == eId)
            .ToListAsync();
        } catch (Exception e) {
            Console.Write(e.StackTrace);
        }
        return RedirectToPage();
    }
}
