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
    public class IndexModel : PageModel
    {
        private readonly HelloWorld.Data.TimecardContext _context;

        public IndexModel(HelloWorld.Data.TimecardContext context)
        {
            _context = context;
        }

        public IList<tester> tester { get;set; } = default!;

        public async Task OnGetAsync()
        {            
            if (_context.tester != null)
            {
                tester = await _context.tester.ToListAsync();
            }
        }
    }
}
