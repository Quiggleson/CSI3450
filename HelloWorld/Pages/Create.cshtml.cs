using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HelloWorld.Data;
using HelloWorld.Models;

namespace HelloWorld.Pages
{
    public class CreateModel : PageModel
    {
        private readonly HelloWorld.Data.TimecardContext _context;

        public CreateModel(HelloWorld.Data.TimecardContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public tester tester { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.tester == null || tester == null)
            {
                return Page();
            }

            _context.tester.Add(tester);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
