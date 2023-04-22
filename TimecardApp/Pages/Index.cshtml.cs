using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TimecardApp.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel()
        {

        }

        public IActionResult OnGet()
        {
            if (TempData.ContainsKey("ETitle") && (string) TempData.Peek("ETitle") == "Manager"){
                return RedirectToPage("/manage");
            } else if (TempData.ContainsKey("ETitle") && (string) TempData.Peek("ETitle") == "Employee"){
                return RedirectToPage("/timeentry");
            }
            if (!TempData.ContainsKey("EId"))
            {
                TempData["Message"] = "You have to login before you can navigate the site.";
                return RedirectToPage("/login");
            }
            return Page();
        }

    }
}