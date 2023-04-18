using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HelloWorld.Pages{
    public class LogoutModel : PageModel {

        public LogoutModel(){

        }

        public IActionResult OnGet(){

            TempData.Clear();

            return RedirectToPage("/login");
        }
    }
}