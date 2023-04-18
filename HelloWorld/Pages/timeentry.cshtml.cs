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
    public class TimeentryModel : PageModel
    {
        private readonly TimecardContext _context;

        public TimeentryModel(TimecardContext context)
        {
            _context = context;
        }

        public IList<TimeRecord> TimeRecords { get; set; }
        public string ErrorMessage { get; set; }
        public bool HasUnresolvedClockIn { get; private set; } = true;

        public async Task<IActionResult> OnGetAsync()
        {
            if (!TempData.ContainsKey("EId"))
            {
                TempData["Message"] = "You have to login before you can navigate the site.";
                return RedirectToPage("/login");
            }
            int eId = Convert.ToInt32(TempData.Peek("EId"));

            TimeRecords = await _context.TimeRecords
                .Where(tr => tr.EId == eId)
                .OrderBy(tr => tr.TimeIn)
                .Take(10)
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            int eId = Convert.ToInt32(TempData.Peek("EId"));
            DateTime currentTime = DateTime.Now;

            bool HasUnresolvedClockIn = await _context.TimeRecords
                .AnyAsync(tr => tr.EId == eId && !tr.TimeOut.HasValue);

            if (Request.Form.ContainsKey("clockin"))
            {
                if (!HasUnresolvedClockIn)
                {
                    TimeRecord timeRecord = new TimeRecord
                    {
                        EId = eId,
                        TimeIn = currentTime
                    };
                    _context.TimeRecords.Add(timeRecord);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Success clocking in";
                }
                else
                {
                    TempData["Warning"] = "Error clocking in, you have an unresolved clock in. If you believe this to be a mistake, please contact your manager";
                    return RedirectToPage();
                }
            }
            else if (Request.Form.ContainsKey("clockOut"))
            {
                TimeRecord? unresolvedClockIn = await _context.TimeRecords
                .Where(tr => tr.EId == eId && tr.TimeIn < DateTime.Now && !tr.TimeOut.HasValue)
                .OrderByDescending(tr => tr.TimeIn)
                .FirstOrDefaultAsync();
                if (HasUnresolvedClockIn && unresolvedClockIn != null)
                {
                    unresolvedClockIn.TimeOut = DateTime.Now;
                    _context.TimeRecords.Update(unresolvedClockIn);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Success clocking out";
                }
                else
                {
                    TempData["Warning"] = "Error clocking out, you must clock in first. If you believe this to be a mistake, please contact your manager.";
                    return RedirectToPage();
                }
            }
            else if (Request.Form.ContainsKey("starRecord"))
            {
                DateTime timeRecordId = DateTime.ParseExact(Request.Form["timeRecordId"], "M/d/yyyy hh:mm:ss tt", null);

                TimeRecord? timeRecord = await _context.TimeRecords
                    .FirstOrDefaultAsync(tr => tr.EId == eId && tr.TimeIn == timeRecordId);

                if (timeRecord != null)
                {
                    timeRecord.IsStarred = (sbyte)(timeRecord.IsStarred == 1 ? 0 : 1);
                    _context.TimeRecords.Update(timeRecord);
                    await _context.SaveChangesAsync();
                }
            } else if (Request.Form.ContainsKey("UnStarRecord"))
            {
                DateTime timeRecordId = DateTime.ParseExact(Request.Form["timeRecordId"], "M/d/yyyy hh:mm:ss tt", null);

                TimeRecord? timeRecord = await _context.TimeRecords
                    .FirstOrDefaultAsync(tr => tr.EId == eId && tr.TimeIn == timeRecordId);

                if (timeRecord != null)
                {
                    timeRecord.IsStarred = null;
                    _context.TimeRecords.Update(timeRecord);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToPage("/Timeentry");
        }
    }
}