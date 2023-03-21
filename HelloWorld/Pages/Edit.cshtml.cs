using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HelloWorld.Data;
using HelloWorld.Models;

namespace HelloWorld.Pages
{
    public class EditModel : PageModel
    {
        private readonly HelloWorld.Data.TimecardContext _context;

        public EditModel(HelloWorld.Data.TimecardContext context)
        {
            _context = context;
        }

        [BindProperty]
        public tester tester { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.tester == null)
            {
                return NotFound();
            }

            var tester =  await _context.tester.FirstOrDefaultAsync(m => m.numba == id);
            if (tester == null)
            {
                return NotFound();
            }
            tester = tester;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Console.Write("not valid");
                return Page();
            }

            _context.Attach(tester).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!testerExists(tester.numba))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool testerExists(int id)
        {
          return (_context.tester?.Any(e => e.numba == id)).GetValueOrDefault();
        }
    }
}
