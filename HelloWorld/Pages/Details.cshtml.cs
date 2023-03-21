using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HelloWorld.Data;
using HelloWorld.Models;

namespace HelloWorld.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly HelloWorld.Data.TimecardContext _context;

        public DetailsModel(HelloWorld.Data.TimecardContext context)
        {
            _context = context;
        }

      public tester tester { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.tester == null)
            {
                return NotFound();
            }

            var tester = await _context.tester.FirstOrDefaultAsync(m => m.numba == id);
            if (tester == null)
            {
                return NotFound();
            }
            else 
            {
                tester = tester;
            }
            return Page();
        }
    }
}
