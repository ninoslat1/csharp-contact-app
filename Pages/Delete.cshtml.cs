using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoginApp.Pages
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
